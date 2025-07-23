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
        #region 常量与字段
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

        // 根节点视图模型
        public MetaLocationRootViewModel Root { get; } = new MetaLocationRootViewModel();
        #endregion

        #region 构造函数
        /// <summary>
        /// 类构造函数
        /// </summary>
        public ExplorerViewModel() : base("Solution Explorer")
        {
            ContentId = ToolContentId;
            _processor = new OneTaskProcessor();
            Workspace.This.ActiveDocumentChanged += OnActiveDocumentChanged;

            // 初始化时加载示例数据
            LoadSampleDataAsyncT();
           
        }
       
        #endregion

        #region 命令
        /// <summary>
        /// 获取用于过滤树视图节点显示的命令
        /// （如果包含过滤字符串则显示节点）
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanExecuteSearch))]
        private async Task SearchAsync(object parameter)
        {
            string findThis = parameter as string;
            if (findThis == null) return;

            await SearchCommand_ExecutedAsync(findThis);
        }

        /// <summary>
        /// 搜索命令的可执行状态判断
        /// </summary>
        private bool CanExecuteSearch(object parameter)
        {
            return !IsProcessing && Root.BackUpCountryRootsCount > 0;
        }
        #endregion

        #region 方法
        private void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            // 激活文档变更时的处理逻辑
        }

        /// <summary>
        /// 实现IDisposable接口
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 从XML文件加载初始示例数据到内存
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
        /// 用过滤字符串过滤树视图中的节点显示
        /// （如果包含过滤字符串则显示节点）
        /// </summary>
        protected async Task<int> SearchCommand_ExecutedAsync(string findThis)
        {
            findThis ??= string.Empty;
            bool shouldRestore = !string.IsNullOrEmpty(_lastSearchText) && string.IsNullOrEmpty(findThis);
            bool isSearchBoxEmptyNow = string.IsNullOrEmpty(findThis);
            bool wasSearchBoxEmptyBefore = string.IsNullOrEmpty(_lastSearchText);

            // 只有在搜索框非空时才记录展开状态
            if (!isSearchBoxEmptyNow && wasSearchBoxEmptyBefore)
            {
                Root.RecordAllExpandStates();
            }

            // 执行搜索逻辑
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

                // 从非空变为空时恢复节点和状态
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
                Console.WriteLine($"搜索执行出错: {exp.Message}");
            }
            finally
            {
                IsProcessing = false;
            }

            return -1;
        }

        /// <summary>
        /// 恢复所有原始节点
        /// </summary>
        private void RestoreAllNodes()
        {
            if (Root == null) return;

            // 清空当前显示的节点
            Root.CountryRootItems.Clear();

            // 从备份中恢复所有根节点
            foreach (var rootItem in Root.BackUpCountryRoots)
            {
                rootItem.RestoreChildrenFromBackup();
                Root.CountryRootItems.Add(rootItem);
            }
        }

        /// <summary>
        /// 重置节点的匹配范围
        /// </summary>
        private void ResetRange(MetaLocationViewModel node)
        {
            node.SetRange(new SelectionRange(-1, -1));

            // 递归处理所有子节点
            foreach (var child in node.BackUpNodes)
            {
                ResetRange(child);
            }
        }

        /// <summary>
        /// 加载示例数据的异步启动方法
        /// </summary>
        public async void LoadSampleDataAsyncT()
        {
            await LoadSampleDataAsync();
        }

        /// <summary>
        /// 清理资源的实际实现
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 释放托管资源
                _processor?.Dispose();
                Workspace.This.ActiveDocumentChanged -= OnActiveDocumentChanged;
            }
            // 释放非托管资源（如果有）
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~ExplorerViewModel()
        {
            Dispose(false);
        }
        #endregion
    }
}