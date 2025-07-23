using AvalonDock;
using BusinessLib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FilterTreeView.Tasks;
using FilterTreeViewLib.Models;
using FilterTreeViewLib.ViewModels;
using FilterTreeViewLib.ViewModels.Tree.Search;
using FilterTreeViewLib.ViewModelsSearch.SearchModels;
using FilterTreeViewLib.ViewModelsSearch.SearchModels.Enums;
using System.Windows;

namespace AC.ViewModels
{
    internal partial class ExplorerViewModel : ToolViewModel
    {
        #region �������ֶ�
        public const string ToolContentId = "Solution Explorer";
        private readonly OneTaskProcessor _processor;
        private string _lastSearchText = string.Empty;

        [ObservableProperty]
        private bool _isProcessing;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _searchString = string.Empty;

        [ObservableProperty]
        private string _statusStringResult = string.Empty;

        [ObservableProperty]
        private bool _isStringContainedSearchOption=true;

        [ObservableProperty]
        private int _countSearchMatches;

        // ���ڵ���ͼģ��
        public MetaLocationRootViewModel Root { get; } = new MetaLocationRootViewModel();
        #endregion

        #region ���캯��
        /// <summary>
        /// �๹�캯��
        /// </summary>
        public ExplorerViewModel() : base("Solution Explorer")
        {
            ContentId = ToolContentId;
            _processor = new OneTaskProcessor();
            Workspace.This.ActiveDocumentChanged += OnActiveDocumentChanged;

            // ��ʼ��ʱ����ʾ������
            LoadSampleDataAsyncT();
           
        }
       
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ���ڹ�������ͼ�ڵ���ʾ������
        /// ��������������ַ�������ʾ�ڵ㣩
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanExecuteSearch))]
        private async Task SearchAsync(object parameter)
        {
            string findThis = parameter as string;
            if (findThis == null) return;

            await SearchCommand_ExecutedAsync(findThis);
        }

        /// <summary>
        /// ��������Ŀ�ִ��״̬�ж�
        /// </summary>
        private bool CanExecuteSearch(object parameter)
        {
            return !IsProcessing && Root.BackUpCountryRootsCount > 0;
        }
        #endregion

        #region ����
        private void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            // �����ĵ����ʱ�Ĵ����߼�
        }

        /// <summary>
        /// ʵ��IDisposable�ӿ�
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// ��XML�ļ����س�ʼʾ�����ݵ��ڴ�
        /// </summary>
        public async Task LoadSampleDataAsync()
        {
            IsProcessing = true;
            IsLoading = true;
            StatusStringResult = "Loading Data... please wait.";

            try
            {
                await Root.LoadData(@"E:\Mirserver");
                await Root.StartMonitoring(@"E:\Mirserver");
                StatusStringResult = $"Searching... '{SearchString}'";
                await SearchCommand_ExecutedAsync(SearchString);
            }
            finally
            {
                IsLoading = false;
                IsProcessing = false;
            }
        }

        /// <summary>
        /// �ù����ַ�����������ͼ�еĽڵ���ʾ
        /// ��������������ַ�������ʾ�ڵ㣩
        /// </summary>
        protected async Task<int> SearchCommand_ExecutedAsync(string findThis)
        {
            findThis ??= string.Empty;
            bool shouldRestore = !string.IsNullOrEmpty(_lastSearchText) && string.IsNullOrEmpty(findThis);
            bool isSearchBoxEmptyNow = string.IsNullOrEmpty(findThis);
            bool wasSearchBoxEmptyBefore = string.IsNullOrEmpty(_lastSearchText);

            // ֻ����������ǿ�ʱ�ż�¼չ��״̬
            if (!isSearchBoxEmptyNow && wasSearchBoxEmptyBefore)
            {
                Root.RecordAllExpandStates();
            }

            // ִ�������߼�
            SearchParams param = new SearchParams(
                findThis,
                IsStringContainedSearchOption ? SearchMatch.StringIsContained : SearchMatch.StringIsMatched
            );

            try
            {
                IsProcessing = true;

                var tokenSource = new CancellationTokenSource();
                Func<int> searchFunc = () => Root.DoSearch(param, tokenSource.Token);
                var resultCount = await _processor.ExecuteOneTask(searchFunc, tokenSource);

                // �ӷǿձ�Ϊ��ʱ�ָ��ڵ��״̬
                if (shouldRestore)
                {
                    RestoreAllNodes();
                    Root.RestoreAllExpandStates();
                    foreach (var rootItem in Root.BackUpCountryRoots)
                    {
                        ResetRange(rootItem);
                    }
                }

                StatusStringResult = findThis;
                CountSearchMatches = resultCount;
                _lastSearchText = findThis;

                return CountSearchMatches;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"����ִ�г���: {exp.Message}");
            }
            finally
            {
                IsProcessing = false;
            }

            return -1;
        }

        /// <summary>
        /// �ָ�����ԭʼ�ڵ�
        /// </summary>
        private void RestoreAllNodes()
        {
            if (Root == null) return;

            // ��յ�ǰ��ʾ�Ľڵ�
            Root.CountryRootItems.Clear();

            // �ӱ����лָ����и��ڵ�
            foreach (var rootItem in Root.BackUpCountryRoots)
            {
                rootItem.RestoreChildrenFromBackup();
                Root.CountryRootItems.Add(rootItem);
            }
        }

        /// <summary>
        /// ���ýڵ��ƥ�䷶Χ
        /// </summary>
        private void ResetRange(MetaLocationViewModel node)
        {
            node.SetRange(new SelectionRange(-1, -1));

            // �ݹ鴦�������ӽڵ�
            foreach (var child in node.BackUpNodes)
            {
                ResetRange(child);
            }
        }

        /// <summary>
        /// ����ʾ�����ݵ��첽��������
        /// </summary>
        public async void LoadSampleDataAsyncT()
        {
            await LoadSampleDataAsync();
        }

        /// <summary>
        /// ������Դ��ʵ��ʵ��
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // �ͷ��й���Դ
                _processor?.Dispose();
                Workspace.This.ActiveDocumentChanged -= OnActiveDocumentChanged;
            }
            // �ͷŷ��й���Դ������У�
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~ExplorerViewModel()
        {
            Dispose(false);
        }
        #endregion
    }
}