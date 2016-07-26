using System;
using System.IO;
using System.Windows.Forms;

namespace BackupProgram
{
    /// <summary>
    /// 静的メソッドのクラス
    /// </summary>
    static class Util
    {
        private static String executable_path = null;
        private static String separator = null;
        private static String main_drive = null;

        /// <summary>
        /// ディレクトリ区切り
        /// </summary>
        public static String Separator
        {
            get
            {
                if(separator == null)
                {
                    separator = Path.DirectorySeparatorChar.ToString();
                }
                return separator;
            }
        }

        /// <summary>
        /// 実行プログラムのパス
        /// </summary>
        public static String ExecutablePath
        {
            get
            {
                if(executable_path == null)
                {
                    String path = Application.ExecutablePath;
                    int pos = path.LastIndexOf(Separator);
                    executable_path = path.Substring(0, pos);
                }
                return executable_path;
            }
        }

        /// <summary>
        /// メインドライブ
        /// </summary>
        public static String MainDrive
        {
            get
            {
                if(main_drive == null)
                {
                    main_drive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                }
                return main_drive;
            }
        }

    }
}
