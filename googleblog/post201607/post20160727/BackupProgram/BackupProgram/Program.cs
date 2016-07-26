using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupProgram
{
    class Program
    {
        private String[] parameter = null;
        /// <summary>
        /// 実行クラスのコンストラクタ
        /// </summary>
        /// <param name="parameter"></param>
        public Program(string[] parameter)
        {
            this.parameter = parameter;
        }
        /// <summary>
        /// 実行メソッド
        /// </summary>
        public void Run()
        {
            String param = parameter[0];
            IniReader ini = new IniReader();
            //登録処理
            if (Object.Equals("APPLY", param.ToUpper()))
            {
                if(parameter.Length != 1)
                {
                    throw new Exception("登録処理中でパラメタが間違っています。");
                }
                new RegApply().Run();
                ini.Apply();
                return;
            }
            //バックアップフォルダーを開く。
            if (Object.Equals("OPEN", param.ToUpper()))
            {
                if(parameter.Length != 2)
                {
                    throw new Exception("バックアップフォルダーを開く中でパラメタが間違っています。");
                }
                System.Diagnostics.Process.Start("explorer.exe", ini.BackupDirectory);
                return;
            }
            //バックアップ
            if(parameter.Length != 1)
            {
                throw new Exception("バックアップ中でパラメタが間違っています。");
            }
            new BackupCopy(parameter[0]).Run(ini);
        }
        /// <summary>
        /// メインスタート
        /// </summary>
        /// <param name="args">実行パラメタ</param>
        static void Main(string[] args)
        {
#if !DEBUG
            args = new String[]
            {
                @"APPLY"
            };
#endif
            try
            {
                if (args.Length != 1 && args.Length != 2)
                {
                    throw new Exception("パラメタが間違っています。");
                }
                new Program(args).Run();
            }
            catch (Exception e)
            {
                IniReader.MessageBox(0, e.ToString(), "ERROR", 0);
            }
#if DEBUG
            Console.WriteLine("Press Any Key...");
            Console.ReadLine();
#endif
        }
    }
}
