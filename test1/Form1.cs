using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            
        }
        /// <summary>
        /// 파라미터로 받은 디렉토리와 FileName을 이용하여 해당 파일을 읽어온다.
        /// </summary>
        /// <param name="directory">읽어올 파일 디렉토리
        /// <param name="fileName">읽어올 파일 이름
        /// <returns>파일이 있을경우 StreamReader 없을경우 Null</returns>
        public StreamReader ReadFile(string mir_directory, string mir_fileName)
        {
            //해당 디렉토리가 존재하지 않으면 Null 리턴
            DirectoryInfo di = new DirectoryInfo(mir_directory);
            if (!di.Exists) { return null; }

            //해당 디렉토리에 파일이 존재 하지 않으면 Null 리턴
            FileInfo fi = new FileInfo(mir_directory + @"\" + mir_fileName);
            if (!fi.Exists) { return null; }

            //파일을 읽어 Buffer에 저장, 한글문장일경우 Encoding을 지정해줘야한다.
            StreamReader sr = new StreamReader(mir_directory + @"\" + mir_fileName, Encoding.Default);
            return sr;
        }


        /// <summary>
        /// ReadToEnd() - 버퍼에 있는 모든 텍스트를 한번에 가져온다.
        /// </summary>
        /// <param name="sr">
        public void MirReadToEnd(StreamReader sr)
        {
            if (sr != null)
            {
                label1.Text = label1.Text+"\n"+sr.ReadToEnd();
            }
        }


        /// <summary>
        /// ReadLie() - 버퍼에 있는 텍스트를 한줄씩 가져온다.
        /// </summary>
        public void MirReadLine(StreamReader sr)
        {
            if (sr != null)
            {
                // Peek() - 버퍼에 있는 다음문자를 int형식으로 복사하여 가져온다.(없을경우 -1을 리턴한다.)
                while (sr.Peek() > -1)
                {
                    label2.Text = label2.Text + "   " + sr.ReadLine();
                }
            }
            
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowFileOpenDialog();
        }
        public string ShowFileOpenDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "파일 오픈 예제창";

            ofd.FileName = "test";

            ofd.Filter = "모든 파일 (*.*) | *.*|그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp;";



            //파일 오픈창 로드

            DialogResult dr = ofd.ShowDialog();



            //OK버튼 클릭시

            if (dr == DialogResult.OK)

            {

                //File명과 확장자를 가지고 온다.

                string fileName = ofd.SafeFileName;

                //File경로와 File명을 모두 가지고 온다.

                string fileFullName = ofd.FileName;

                //File경로만 가지고 온다.

                string filePath = fileFullName.Replace(fileName, "");



                //출력 예제용 로직
                
                label3.Text = "Full Name  : " + fileFullName;
                


                //c:\test.txt 파일을 읽어 sr에 저장
                StreamReader sr = ReadFile(filePath, "test.txt");
                if (sr != null)
                {
                    //ReadToEnd 메소드 호출
                    MirReadToEnd(sr);
                    //버퍼 닫기
                    sr.Close();
                }

                //c:\test.txt 파일을 읽어 sr에 저장
                sr = ReadFile(filePath, "test.txt");
                if (sr != null)
                {
                    //ReadLine 메소드 호출
                    MirReadLine(sr);
                    //버퍼 닫기
                    sr.Close();
                }


                //File경로 + 파일명 리턴

                return fileFullName;

            }

            //취소버튼 클릭시 또는 ESC키로 파일창을 종료 했을경우

            else if (dr == DialogResult.Cancel)
            {
                return "";

            }
            return "";
        }
    }
}
