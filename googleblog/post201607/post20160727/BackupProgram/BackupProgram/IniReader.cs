using System;
using System.Text;
using System.Runtime.InteropServices;

namespace BackupProgram
{
    /// <summary>
    /// Com+のプロファイル扱うメソッドおよびWindowのポップアップ
    /// </summary>
    class IniReader
    {
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(String section, String key, String val, String filepath);

        [DllImport("kernel32.dll")]
        private static extern long GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filepath);

        [DllImport("User32.dll")]
        public static extern int MessageBox(int hWnd, String lpText, String lpCaption, int uType);

        private const String SECTION = "CONFIG";
        private static class CONFIG
        {
            public const String DIR = "BACKUP_DIR";
            public const String TYPE = "TYPE";
        }

        private const String INI_NAME_BACKUP = "Backup";
        private const String CONFIG_NAME = "config.ini";

        public static String FILE_MOVE = "M";
        public static String FILE_COPY = "C";

        private String DEFAULT_DIRECTORY = Util.ExecutablePath + Util.Separator + INI_NAME_BACKUP;
        private const int BUFFER_SIZE = 4096;

        private String backup_directory = null;
        private String process_type = null;

        /// <summary>
        /// 基本環境ファイルを生成
        /// </summary>
        public void Apply()
        {
            //バックアップするバスを生成
            WritePrivateProfileString(SECTION, CONFIG.DIR, DEFAULT_DIRECTORY, Util.ExecutablePath + Util.Separator + CONFIG_NAME);
            //バックアップするタイプを生成
            //M : ファイル移動
            //C : ファイルコピー
            WritePrivateProfileString(SECTION, CONFIG.TYPE, FILE_MOVE, Util.ExecutablePath + Util.Separator + CONFIG_NAME);
        }
        /// <summary>
        /// 環境ファイルからバックアップするパスを取得する。
        /// </summary>
        public String BackupDirectory
        {
            get
            {
                if (backup_directory == null)
                {
                    StringBuilder sb = new StringBuilder();
                    GetPrivateProfileString(SECTION, CONFIG.DIR, DEFAULT_DIRECTORY, sb, BUFFER_SIZE, Util.ExecutablePath + Util.Separator + CONFIG_NAME);
                    backup_directory = sb.ToString();
                }
                return backup_directory;
            }
        }
        /// <summary>
        /// 環境ファイルからバックアップタイプを取得する。
        /// </summary>
        public String ProcessType
        {
            get
            {
                if(process_type == null)
                {
                    StringBuilder sb = new StringBuilder();
                    GetPrivateProfileString(SECTION, CONFIG.TYPE, FILE_MOVE, sb, BUFFER_SIZE, Util.ExecutablePath + Util.Separator + CONFIG_NAME);
                    process_type = sb.ToString();
                }
                return process_type;
            }
        }
    }
}
