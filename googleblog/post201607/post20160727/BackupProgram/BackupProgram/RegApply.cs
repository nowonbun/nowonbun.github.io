using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;


namespace BackupProgram
{
    /// <summary>
    /// 初期レジストリクラス(WindowのファイルとディレクトリのContextMenu追加)
    /// </summary>
    class RegApply
    {
        private int APPLY_COUNT = 0;

        private const String REG_NAME_BACKUP = "Backup";

        private const String PARAMETER_OPTION = " \"%V\"";

        private const String COMMAND_PATH = "command";

        private String[] MENU_NAME = new String[]
        {
            "Directory" + Util.Separator + "shell" + Util.Separator + REG_NAME_BACKUP,
            "Directory" + Util.Separator + "Background" + Util.Separator + "shell" + Util.Separator + REG_NAME_BACKUP,
            "*" + Util.Separator + "shell" + Util.Separator + REG_NAME_BACKUP
        };

        private String[] COMMANDER;

        private String HKEY_CLASSES_ROOT = "HKEY_CLASSES_ROOT" + Util.Separator;

        private String FileName;

        /// <summary>
        /// コンストラクタ(メンバー変数初期化)
        /// </summary>
        public RegApply()
        {
            String code = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            FileName = System.IO.Path.GetFileName(code);
            APPLY_COUNT = MENU_NAME.Length;
            COMMANDER = new String[APPLY_COUNT];
            for (int i = 0; i < APPLY_COUNT; i++)
            {
                COMMANDER[i] = MENU_NAME[i] + Util.Separator + COMMAND_PATH;
            }
        }

        /// <summary>
        /// 実行
        /// </summary>
        public void Run()
        {
            for (int i = 0; i < APPLY_COUNT; i++)
            {
                PermissionReg(MENU_NAME[i], COMMANDER[i]);
                DeleteReg(MENU_NAME[i], COMMANDER[i]);
                CreateReg(MENU_NAME[i], COMMANDER[i]);
            }
        }

        /// <summary>
        /// 修正するレジストリに権限を与える。
        /// </summary>
        /// <param name="menuname">ルートレジストリパス</param>
        /// <param name="command">命令レジストリパス</param>
        private void PermissionReg(String menuname, String command)
        {
            try
            {
                RegistryPermission regPerm = new RegistryPermission(
                    RegistryPermissionAccess.Write,
                    HKEY_CLASSES_ROOT + menuname);

                regPerm.AddPathList(
                    RegistryPermissionAccess.Write,
                    HKEY_CLASSES_ROOT + command);

                regPerm.Demand();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// 既存レジストリ削除
        /// </summary>
        /// <param name="menuname">ルートレジストリパス</param>
        /// <param name="command">命令レジストリパス</param>
        private void DeleteReg(String menuname, String command)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKey(command);
                Registry.ClassesRoot.DeleteSubKey(menuname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// レジストリ登録
        /// </summary>
        /// <param name="menuname">ルートレジストリパス</param>
        /// <param name="command">命令レジストリパス</param>
        private void CreateReg(String menuname, String command)
        {
            try
            {
                using (RegistryKey regmenu = Registry.ClassesRoot.CreateSubKey(menuname))
                {
                    regmenu.SetValue(String.Empty, REG_NAME_BACKUP);
                }
                using (RegistryKey regcmd = Registry.ClassesRoot.CreateSubKey(command))
                {
                    regcmd.SetValue(String.Empty, Util.ExecutablePath + Util.Separator + FileName + PARAMETER_OPTION);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }


    }
}
