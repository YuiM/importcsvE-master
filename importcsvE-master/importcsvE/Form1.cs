using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;


namespace importcsvE
{
    public partial class Form1 : Form
    {
        public string filename = "";
        public DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        //ファイルダイアログを開く関数
        private void openfile()
        {
            openFileDialog1.FileName = "defalt.csv";
            openFileDialog1.Filter = "csvファイル(*.csv;*.txt)|*.csv;*.txt|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            openFileDialog1.FilterIndex = 2;
            //タイトルを設定する
            openFileDialog1.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            openFileDialog1.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            openFileDialog1.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            openFileDialog1.CheckPathExists = true;

            //ダイアログを表示する
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(openFileDialog1.FileName);
                filename = openFileDialog1.FileName;

            }




        }
        private void button1_Click(object sender, EventArgs e)
        {
            openfile();

            //ファイル名を渡してDataTableに読み込む。

            dt = ReadFileCsv(filename);
            //読み込み終了
            MessageBox.Show("csvファイルの読み込みに終了しました");


        }


        //csvファイルを書き出す
        private void WriteFileCsv(DataTable dt, String filename, Boolean writeHeader)
        {
            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc =
                System.Text.Encoding.GetEncoding("Shift_JIS");

            //書き込むファイルを開く
            System.IO.StreamWriter sr =
                new System.IO.StreamWriter(filename, false, enc);

            int colCount = dt.Columns.Count;
            int lastColIndex = colCount - 1;

            //ヘッダを書き込む
            if (writeHeader)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //ヘッダの取得
                    string field = dt.Columns[i].Caption;
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sr.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i)
                    {
                        sr.Write(',');
                    }
                }
                //改行する
                sr.Write("\r\n");
            }

            //レコードを書き込む
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //フィールドの取得
                    string field = row[i].ToString();
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sr.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i)
                    {
                        sr.Write(',');
                    }
                }
                //改行する
                sr.Write("\r\n");
            }

            //閉じる
            sr.Close();

            //書き込み終了
            MessageBox.Show("csvファイルの書き込みに終了しました");


        }


        /// <summary>
        /// 必要ならば、文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }





        //Fileの中身を読み込む

        private DataTable ReadFileCsv(String fileName)
        {

            //Linq 
            string[] data;
            System.Text.Encoding encord = System.Text.Encoding.GetEncoding("Shift_JIS");

            dt = new DataTable();

            dt.Columns.Add("bangou1");//bangou1
            dt.Columns.Add("bangou2");//bangou2
            dt.Columns.Add("filed1");
            dt.Columns.Add("filed2");
            dt.Columns.Add("filed3");
            dt.Columns.Add("filed4");
            dt.Columns.Add("filed5");
            dt.Columns.Add("filed6");
            dt.Columns.Add("filed7");
            dt.Columns.Add("filed8");
            dt.Columns.Add("filed9");
            //dt.Columns.Add("filed10");
            
            dt.Columns.Add("DateTime");//日付


            TextFieldParser parser = new TextFieldParser(fileName, encord);

            parser.TextFieldType = FieldType.Delimited;

            //区切り文字はコンマ
            parser.SetDelimiters(",");

            //データがあるかを確認します。

            /*if (!parser.EndOfData)
            {
                //csvファイルから１行読み取ります。
                data = parser.ReadFields();

                //カラムの数を取得します。

                int cols = data.Length;
                //カラム数が0ならReturnする。
                if(cols == 0)
                {
                    return null;
                }

                for (int i = 0; i < cols; i++)
                {
                    //カラム名をセットします。
                    dt.Columns.Add(data[i]);
                }
                //parserをもどす
               

            }*/


            //csvをデータテーブルに格納
            while (!parser.EndOfData)
            {
                //1行読み込む
                data = parser.ReadFields();
                //１行DataRowに追加
                DataRow row = dt.NewRow();
                row["bangou1"] = data[0];
                row["bangou2"] = data[1];


                row["filed1"] = textBox1.Text;
                row["filed2"] = textBox2.Text;
                row["filed3"] = textBox3.Text;
                row["filed4"] = textBox4.Text;
                row["filed5"] = textBox5.Text;
                row["filed6"] = textBox6.Text;
                row["filed7"] = textBox7.Text;
                row["filed8"] = textBox8.Text;
                row["filed9"] = textBox9.Text;

               
                row["DateTime"] = dateTimePicker1.Text;

                dt.Rows.Add(row);

            }

            parser.Dispose();

            return dt;
        }


        //ファイルを書き出す。
        private void button1_Click_1(object sender, EventArgs e)
        {

            //dtをcsvファイルに書き出す。
            //書き出す場所を指定するためのfiledialogをおーぷんする　。


            //bool ret = 
            openfile();
            WriteFileCsv(dt, filename, false);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            openfile();
            //ファイル名だけしゅとくする　。
            filename = System.IO.Path.GetFileName(filename);

            MessageBox.Show(filename);


            //ファイルを5MB単位で分割する。
            //SplitFile();
            //split用のJscriptを実行する。分割元のファイル名を引数として
            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo();

            psi.FileName = "split.js";
            psi.Arguments = @filename;
            //アプリケーションを起動する
            System.Diagnostics.Process.Start(psi);

            //書き込み終了
            MessageBox.Show("csvファイルの分割に終了しました");



        }

        //以下は使ってない。
        private void SplitFile()
        {
            // バイナリ・ファイルの読み込み
            System.IO.StreamReader cReader = new StreamReader(filename, System.Text.Encoding.Default);
            // 読み込んだ結果をすべて格納するための変数を宣言する
            string stResult = string.Empty;

            int count = 0;
            int splitSize = 10000;
            int countIndex = 1;

            while (cReader.Peek() >= 0)
            {
                //ファイルを１行ずつ読み込む
                string strBuffer = cReader.ReadLine();
                stResult += strBuffer + System.Environment.NewLine;
                count++;
                //１００００行単位でファイルに書き出す。
                if (count == splitSize * countIndex)
                {
                    // 出力ファイル名（out0001.csv、out0002.csv、……）
                    string name = String.Format("out{0:D4}.csv", countIndex + 1);
                    countIndex++;
                    //書き出す。
                    File.WriteAllText(name, stResult);
                    //書き込んだら初期化
                    stResult = string.Empty;
                }

            }

            //残りを書き出す。
            String lastfile = String.Format("out{0:D4}.csv", countIndex + 1);
            File.WriteAllText(lastfile, stResult);



            /*for (int remain = src.Length; remain > 0; remain -= FILESIZE)
            {

                // 作成する分割ファイルの実際のサイズ
                int length = Math.Min(FILESIZE, remain);

                // 分割ファイルへ書き出すbyte配列の作成
                byte[] dest = new byte[length];
                Array.Copy(src, num * FILESIZE, dest, 0, length);

                // 出力ファイル名（out0001.csv、out0002.csv、……）
                string name = String.Format("out{0:D4}.csv", num + 1);

                // byte配列のファイルへの書き込み
                File.WriteAllBytes(name, dest);

                num++;
            }*/

            //書き込み終了
            MessageBox.Show("csvファイルの分割に終了しました");

        }
    }
}

