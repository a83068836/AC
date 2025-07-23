using AvalonEditB.Document;
using AvalonEditB.Highlighting;
using AvalonEditB.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HL.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace AC.ViewModels
{
    partial class FileViewModel : PaneViewModel
    {
        //private static ImageSourceConverter ISC = new ImageSourceConverter();

        #region fields
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FileName))]
        [NotifyPropertyChangedFor(nameof(Title))]
        private string _filePath;

        [ObservableProperty]
        private TextDocument _document;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FileName))]
        [NotifyPropertyChangedFor(nameof(Title))]
        private bool _isDirty;

        [ObservableProperty]
        private bool _isReadOnly;

        [ObservableProperty]
        private string _isReadOnlyReason = string.Empty;

        private ICommand _highlightingChangeCommand;

        [ObservableProperty]
        private IHighlightingDefinition _highlightingDefinition;
        #endregion fields
        /// <summary>
        /// Class constructor from file path.
        /// </summary>
        /// <param name="filePath"></param>
        public FileViewModel(string filePath)
        {
            FilePath = filePath;
            Title = FileName;
            
        }

        /// <summary>
        /// Default class constructor
        /// </summary>
        public FileViewModel()
        {
            Document = new TextDocument();
            FilePath = "无标题.txt";

            HighlightingDefinition = null;
            var hlManager = Base.ModelBase.GetService<IThemedHighlightingManager>();
            string extension = System.IO.Path.GetExtension(".txt");
            HighlightingDefinition = hlManager.GetDefinitionByExtension(extension);;
            Title = FileName;
        }

        partial void OnFilePathChanged(string value)
        {
            LoadDocument(value);
        }

        #region properties
        public string FileName
        {
            get
            {
                if (FilePath == null)
                    return "Noname" + (IsDirty ? "*" : "");

                return System.IO.Path.GetFileName(FilePath) + (IsDirty ? "*" : "");
            }
        }


        #region Highlighting Definition
        /// <summary>
        /// Gets a copy of all highlightings.
        /// </summary>
        public ReadOnlyCollection<IHighlightingDefinition> HighlightingDefinitions
        {
            get
            {
                var hlManager = HighlightingManager.Instance;

                if (hlManager != null)
                    return hlManager.HighlightingDefinitions;

                return null;
            }
        }

        /// <summary>
        /// Gets a command that changes the currently selected syntax highlighting in the editor.
        /// </summary>
        public ICommand HghlightingChangeCommand
        {
            get
            {
                if (_highlightingChangeCommand == null)
                {
                    _highlightingChangeCommand = new RelayCommand<object>((p) =>
                    {
                        var parames = p as object[];

                        if (parames == null)
                            return;

                        if (parames.Length != 1)
                            return;

                        var param = parames[0] as IHighlightingDefinition;
                        if (param == null)
                            return;

                        HighlightingDefinition = param;
                    });
                }

                return _highlightingChangeCommand;
            }
        }


        internal void OnAppThemeChanged(IThemedHighlightingManager hlManager)
        {

            if (hlManager == null)
                return;

            // Does this highlighting definition have an associated highlighting theme?
            if (hlManager.CurrentTheme.HlTheme != null)
            {
                
            }

            // 1st try: Find highlighting based on currently selected highlighting
            // The highlighting name may be the same as before, but the highlighting theme has just changed
            if (HighlightingDefinition != null)
            {
                // Reset property for currently select highlighting definition
                HighlightingDefinition = hlManager.GetDefinition(HighlightingDefinition.Name);

                if (HighlightingDefinition != null)
                    return;
            }

            // 2nd try: Find highlighting based on extension of file currenlty being viewed
            if (string.IsNullOrEmpty(FilePath))
                return;

            string extension = System.IO.Path.GetExtension(FilePath);

            if (string.IsNullOrEmpty(extension))
                return;

            // Reset property for currently select highlighting definition
            HighlightingDefinition = hlManager.GetDefinitionByExtension(extension);
        }
        #endregion Highlighting Definition
        #endregion properties

        #region methods
        public bool LoadDocument(string paramFilePath)
        {
            if (File.Exists(paramFilePath))
            {
                Document = new TextDocument();
                IsDirty = false;
                IsReadOnly = false;

                // Check file attributes and set to read-only if file attributes indicate that
                if ((System.IO.File.GetAttributes(paramFilePath) & FileAttributes.ReadOnly) != 0)
                {
                    IsReadOnly = true;
                    IsReadOnlyReason = "This file cannot be edit because another process is currently writting to it.\n" +
                                       "Change the file access permissions or save the file in a different location if you want to edit it.";
                }

                try
                {
                    var fileEncoding = FileEncodings.GetType(paramFilePath);

                    using (FileStream fs = new FileStream(paramFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader reader = FileReader.OpenStream(fs, fileEncoding))
                        {
                            Document = new TextDocument(reader.ReadToEnd());
                            //FileEncoding = reader.CurrentEncoding; // assign encoding after ReadToEnd() so that the StreamReader can autodetect the encoding
                        }
                    }

                    HighlightingDefinition = null;
                    var hlManager = Base.ModelBase.GetService<IThemedHighlightingManager>();

                    //hlManager.SetCurrentTheme("Dark");

                    string extension = System.IO.Path.GetExtension(paramFilePath);
                    HighlightingDefinition = hlManager.GetDefinitionByExtension(extension);
                    ContentId = FilePath;
                    Document.FileName = paramFilePath;
                }
                catch (System.Exception exc)
                {
                    IsReadOnly = true;
                    IsReadOnlyReason = exc.Message;
                    Document.Text = string.Empty;

                    FilePath = string.Empty;
                    
                    HighlightingDefinition = null;
                }

                return true;
            }

            return false;
        }
        #endregion methods

        [RelayCommand]
        public void Close()
        {
            Workspace.This.Close(this);
        }

        public bool CanClose => true;
    }
    #region 获取文件编码
    public class FileEncodings
    {
        /// <summary> 
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型 
        /// </summary> 
        /// <param name=“FILE_NAME“>文件路径</param> 
        /// <returns>文件的编码类型</returns> 
        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        /// <summary> 
        /// 通过给定的文件流，判断文件的编码类型 
        /// </summary> 
        /// <param name=“fs“>文件流</param> 
        /// <returns>文件的编码类型</returns> 
        public static System.Text.Encoding GetType(FileStream fs)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding reVal = Encoding.GetEncoding("gb2312");
            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;

        }

        /// <summary> 
        /// 判断是否是不带 BOM 的 UTF8 格式 
        /// </summary> 
        /// <param name=“data“></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;

        }


    }

    #endregion
}