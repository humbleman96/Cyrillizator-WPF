using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


//--< using >--
using Microsoft.Win32; //FileDialog
using WinForms = System.Windows.Forms; //FolderDialog
using System.IO; //Folder, Directory
using System.Diagnostics; //Debug.WriteLine
using Path = System.IO.Path;
using System.Text.RegularExpressions;
//--</ using >--

namespace Cyrillizator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FolderPath { get; set; }
        public List<string> FileNames { get; set; }
        public Dictionary<string, char> Letters { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenFolderDialog(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;
                tbFolderPath.Text = sPath;
                FolderPath = sPath;

                FileNames = new List<string>();
                DirectoryInfo dir = new DirectoryInfo(FolderPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    FileNames.Add(file.Name);
                }
            }
         
            lvFolderFiles.ItemsSource = FileNames;
        }

        private void Cyrillize(object sender, RoutedEventArgs e)
        {
            if (FolderPath is null)
            {
                MessageBox.Show("Избери папка!", "Кирилизатор", MessageBoxButton.OK, MessageBoxImage.Information); ;
            }

            else
            {
                FileNames = new List<string>();
                Letters = InitilizeCyrillizator();
                DirectoryInfo dir = new DirectoryInfo(FolderPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                    string newFileName = string.Empty;

                    if (Regex.IsMatch(fileNameWithoutExtension.ToString(), @"\p{IsCyrillic}"))
                    {
                        MessageBox.Show("Книгите вече са преведени, изберете друга папка!", "Кирилизатор", MessageBoxButton.OK, MessageBoxImage.Information); ;
                        break;
                    }

                    for (int i = 0; i < fileNameWithoutExtension.Length; i++)
                    {
                        char currentCharacter = fileNameWithoutExtension[i];

                        if (i != fileNameWithoutExtension.Length - 1)
                        {
                            if (currentCharacter.Equals('Z') &&
                               fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "Zh";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('z') &&
                                fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "zh";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('T') &&
                                fileNameWithoutExtension[i + 1].Equals('s'))
                            {
                                string letterCombo = "Ts";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('t') &&
                                fileNameWithoutExtension[i + 1].Equals('s'))
                            {
                                string letterCombo = "ts";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('C') &&
                                fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "Ch";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('c') &&
                                fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "ch";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('S') &&
                               fileNameWithoutExtension[i + 1].Equals('h') &&
                               fileNameWithoutExtension[i + 2].Equals('t')
                               )
                            {
                                string letterCombo = "Sht";
                                newFileName += Letters[letterCombo];
                                i = i + 2;
                                continue;
                            }

                            if (currentCharacter.Equals('s') &&
                                fileNameWithoutExtension[i + 1].Equals('h') &&
                                fileNameWithoutExtension[i + 2].Equals('t'))
                            {
                                string letterCombo = "sht";
                                newFileName += Letters[letterCombo];
                                i = i + 2;
                                continue;
                            }

                            if (currentCharacter.Equals('S') &&
                                fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "Sh";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('s') &&
                                fileNameWithoutExtension[i + 1].Equals('h'))
                            {
                                string letterCombo = "sh";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('J') &&
                                fileNameWithoutExtension[i + 1].Equals('u'))
                            {
                                string letterCombo = "Ju";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('j') &&
                                fileNameWithoutExtension[i + 1].Equals('u'))
                            {
                                string letterCombo = "ju";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('J') &&
                                fileNameWithoutExtension[i + 1].Equals('a'))
                            {
                                string letterCombo = "Ja";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }

                            if (currentCharacter.Equals('j') &&
                                fileNameWithoutExtension[i + 1].Equals('a'))
                            {
                                string letterCombo = "ja";
                                newFileName += Letters[letterCombo];
                                i++;
                                continue;
                            }
                        }

                        if (char.IsLetter(currentCharacter))
                        {
                            newFileName += Letters[currentCharacter.ToString()];
                        }

                        else
                        {
                            newFileName += currentCharacter;
                        }

                    }

                    newFileName += file.Extension;
                    FileNames.Add(newFileName);
                    File.Move(dir + "\\" + file.Name, dir + "\\" + newFileName);
                }

                lvFolderFiles.ItemsSource = FileNames;
            }
        }

        private void RemoveSpecials(object sender, RoutedEventArgs e)
        {
            if (FolderPath is null)
            {
                MessageBox.Show("Избери папка!", "Кирилизатор", MessageBoxButton.OK, MessageBoxImage.Information); ;
            }

            else
            {
                FileNames = new List<string>();             
                DirectoryInfo dir = new DirectoryInfo(FolderPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                    string newFileName = string.Empty;

                    for (int i = 0; i < fileNameWithoutExtension.Length; i++)
                    {
                        char currentCharacter = fileNameWithoutExtension[i];

                        if (currentCharacter.Equals('_'))
                        {
                            newFileName += ' ';
                        }

                        else
                        {
                            newFileName += currentCharacter;
                        }

                    }

                    newFileName += file.Extension;
                    FileNames.Add(newFileName);
                    File.Move(dir + "\\" + file.Name, dir + "\\" + newFileName);
                }

                lvFolderFiles.ItemsSource = FileNames;
            }
        }

        private Dictionary<string, char> InitilizeCyrillizator()
        {
            Letters = new Dictionary<string, char>();

            Letters.Add("A",'А');
            Letters.Add("B",'Б');
            Letters.Add("V",'В');
            Letters.Add("G",'Г');
            Letters.Add("D",'Д');
            Letters.Add("E",'Е');
            Letters.Add("Zh",'Ж');
            Letters.Add("Z",'З');
            Letters.Add("I",'И');
            Letters.Add("J",'Й');
            Letters.Add("K",'К');
            Letters.Add("C", 'К');
            Letters.Add("L",'Л');
            Letters.Add("M",'М');
            Letters.Add("N",'Н');
            Letters.Add("O",'О');
            Letters.Add("P",'П');
            Letters.Add("R",'Р');
            Letters.Add("S",'С');
            Letters.Add("T",'Т');
            Letters.Add("U",'У');
            Letters.Add("F",'Ф');
            Letters.Add("H",'Х');
            Letters.Add("Ts",'Ц');
            Letters.Add("Ch",'Ч');
            Letters.Add("Sh",'Ш');
            Letters.Add("Sht",'Щ');
            Letters.Add("Y",'Ъ');
            Letters.Add("X",'Ь');
            Letters.Add("W", 'У');
            Letters.Add("Ju",'Ю');
            Letters.Add("Ja",'Я');

            Letters.Add("a", 'а');
            Letters.Add("b", 'б');
            Letters.Add("v", 'в');
            Letters.Add("g", 'г');
            Letters.Add("d", 'д');
            Letters.Add("e", 'е');
            Letters.Add("zh", 'ж');
            Letters.Add("z", 'з');
            Letters.Add("i", 'и');
            Letters.Add("j", 'й');
            Letters.Add("k", 'к');
            Letters.Add("c", 'к');
            Letters.Add("l", 'л');
            Letters.Add("m", 'м');
            Letters.Add("n", 'н');
            Letters.Add("o", 'о');
            Letters.Add("p", 'п');
            Letters.Add("r", 'р');
            Letters.Add("s", 'с');
            Letters.Add("t", 'т');
            Letters.Add("u", 'у');
            Letters.Add("f", 'ф');
            Letters.Add("h", 'х');
            Letters.Add("ts", 'ц');
            Letters.Add("ch", 'ч');
            Letters.Add("sh", 'ш');
            Letters.Add("sht", 'щ');
            Letters.Add("y", 'ъ');
            Letters.Add("x", 'ь');
            Letters.Add("w", 'у');
            Letters.Add("ju", 'ю');
            Letters.Add("ja", 'я');

            return Letters;
        }
    }
}
