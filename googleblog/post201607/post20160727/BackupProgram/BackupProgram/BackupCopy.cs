using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BackupProgram
{
    /// <summary>
    /// バックアップクラス
    /// </summary>
    class BackupCopy
    {
        private String parameter = null;
        /// <summary>
        /// コンストラクタ設定
        /// </summary>
        /// <param name="parameter">バックアップする対象のパス</param>
        public BackupCopy(String parameter)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="ini">環境ファイル設定値</param>
        public void Run(IniReader ini)
        {
            String type = ini.ProcessType;
            DateTime time = DateTime.Now;
            String path = ini.BackupDirectory + String.Format("{0}{1:0000}{2:00}{3:00}", Util.Separator, time.Year, time.Month, time.Day);
            String item = CheckItem(parameter);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileAttributes attr = File.GetAttributes(item);
            if (Object.Equals(attr & FileAttributes.Directory, FileAttributes.Directory))
            {
                DirectoryCopy(item, path, type);
            }
            else
            {
                FileCopy(item, path, type);
            }
        }

        /// <summary>
        /// ディレクトリ生成
        /// </summary>
        /// <param name="name">対象のディレクトリパス</param>
        /// <param name="destinationDir">バックアップするところのパス</param>
        /// <returns></returns>
        private String CreateDirectory(String name, String destinationDir)
        {
            String directoryName = name;
            String buffer = directoryName;
            for (int i = 1; Directory.Exists(destinationDir + Util.Separator + buffer); i++)
            {
                buffer = directoryName + "_" + i;
            }
            directoryName = destinationDir + Util.Separator + buffer;
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }

        /// <summary>
        /// バックアップを行う(ディレクトリ)
        /// </summary>
        /// <param name="targetDir">対象ディレクトリ</param>
        /// <param name="destinationDir">バックアップするところのパス</param>
        /// <param name="pType">バックアップタイプ C:コピー M:移動</param>
        private void DirectoryCopy(String targetDir, String destinationDir, String pType)
        {
            DirectoryInfo info = new DirectoryInfo(targetDir);
            String directoryName = CreateDirectory(info.Name, destinationDir);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo[] directorys = info.GetDirectories();

            foreach (FileInfo file in files)
            {
                FileCopy(file.FullName, directoryName, pType);
            }

            foreach (DirectoryInfo directory in directorys)
            {
                DirectoryCopy(directory.FullName, directoryName, pType);
            }
            if (!Object.Equals(pType, IniReader.FILE_COPY))
            {
                info.Delete();
            }
        }

        /// <summary>
        /// バックアップを行う(ファイル)
        /// <param name="name">対象のディレクトリパス</param>
        /// /// <param name="ext">拡張子</param>
        /// <param name="destinationPath">バックアップするところのパス</param>
        /// <returns></returns>
        private String CreateFile(String name, String ext, String destinationPath)
        {
            String fileName = name;
            int pos = fileName.LastIndexOf('.');
            String buffer;
            if (pos > -1)
            {
                buffer = fileName.Substring(0, pos);
            }
            else
            {
                buffer = fileName;
            }

            for (int i = 0; File.Exists(destinationPath + Util.Separator + buffer + ext); i++)
            {
                buffer = fileName.Substring(0, pos) + "(" + i + ")";
            }
            fileName = buffer + ext;

            return fileName;
        }

        private void FileCopy(String targetPath, String destinationPath, String pType)
        {
            FileInfo info = new FileInfo(targetPath);

            String fileName = CreateFile(info.Name, info.Extension, destinationPath);

            if (Object.Equals(pType, IniReader.FILE_COPY))
            {
                info.CopyTo(destinationPath + Util.Separator + fileName, false);
            }
            else
            {
                info.MoveTo(destinationPath + Util.Separator + fileName);
            }
        }


        private String CheckItem(String path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            if (Directory.Exists(path))
            {
                return path;
            }
            throw new Exception("バックアップ対象を設定してないです。");
        }
    }
}
