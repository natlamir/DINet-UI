using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DINet_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void DragDrop(TextBox textbox, object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textbox.Text = s[0];
        }

        private void txtInputVideo_DragEnter(object sender, DragEventArgs e)
        {
            DragEnter(sender, e);
        }

        private void txtInputVideo_DragDrop(object sender, DragEventArgs e)
        {
            DragDrop(txtInputVideo, sender, e);
        }        

        private void txtInputAudio_DragEnter(object sender, DragEventArgs e)
        {
            DragEnter(sender, e);
        }

        private void txtInputAudio_DragDrop(object sender, DragEventArgs e)
        {
            DragDrop(txtInputAudio, sender, e);
        }

        private void ShowFileName(TextBox textBox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
                textBox.Text = openFileDialog1.FileName;
        }

        private void btnBrowseInputVideo_Click(object sender, EventArgs e)
        {
            ShowFileName(txtInputVideo);
        }

        private void btnBrowseInputAudio_Click(object sender, EventArgs e)
        {
            ShowFileName(txtInputAudio);
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtInputAudio.Text) || string.IsNullOrWhiteSpace(txtInputVideo.Text))
            {
                MessageBox.Show("Need input audio and video");
                return;
            }
            try
            {
                txtShellOutput.Clear();
                txtLog.Clear();
                btnGenerate.Enabled = false;
                btnBrowseInputAudio.Enabled = false;
                btnBrowseInputVideo.Enabled = false;
                txtInputAudio.Enabled = false;
                txtInputVideo.Enabled = false;

                string condaEnv = ConfigurationManager.AppSettings["CondaEnvironmentName"];
                string openFaceDir = ConfigurationManager.AppSettings["OpenFaceDirectory"].TrimEnd(new char[] { '\\', '/' });
                string dinetDir = ConfigurationManager.AppSettings["DINetDirectory"].TrimEnd(new char[] { '\\', '/' });
                string anacondaScriptsPath = ConfigurationManager.AppSettings["AnacondaScriptsPath"].TrimEnd(new char[] { '\\', '/' });
                string csvDir = $"{dinetDir}\\asserts\\input";
                string vidFileWithoutExtension = Path.GetFileNameWithoutExtension(txtInputVideo.Text);
                string vidFolderPath = Path.GetDirectoryName(txtInputVideo.Text) + "\\" + vidFileWithoutExtension;

                string exePath = openFaceDir + "\\featureextraction.exe";
                string parameters = $"-f \"{txtInputVideo.Text}\" -out_dir \"{csvDir}\"";
                string command = $"{exePath}  {parameters}";

                if(Directory.Exists(vidFolderPath))
                {
                    MessageBox.Show("Existing directory found: " + Environment.NewLine + vidFolderPath + Environment.NewLine + "Please manually delete this directory or re-name it if it is a directory you want to keep. The DINet uses this directory name to generate the images, and then the app's cleanup process will delete this direcotry.");
                    txtLog.AppendText("Existing directory found: " + Environment.NewLine + vidFolderPath + Environment.NewLine + "Please manually delete this directory or re-name it if it is a directory you want to keep. The DINet uses this directory name to generate the images, and then the app's cleanup process will delete this direcotry.");
                    return;
                }

                if(!File.Exists(anacondaScriptsPath + "\\activate.bat"))
                {
                    MessageBox.Show("In the .config file, the value for 'AnacondaScriptsPath' of: " + Environment.NewLine + anacondaScriptsPath + Environment.NewLine + " is not valid. Update the value and re-launch the app.");
                    txtLog.AppendText("In the .config file, the value for 'AnacondaScriptsPath' of: " + Environment.NewLine + anacondaScriptsPath + Environment.NewLine + " is not valid. Update the value and re-launch the app.");
                    return;
                }

                txtLog.AppendText("Calling OpenFace to generate CSV..." + Environment.NewLine);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    process.Start();

                    process.StandardInput.WriteLine(command);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    await Task.Run(() =>
                    {
                        // Read and display the output of the process in real-time
                        while (!process.StandardOutput.EndOfStream)
                        {
                            string outputLine = process.StandardOutput.ReadLine();
                            Console.WriteLine("OpenFace: " + outputLine);

                            txtShellOutput.Invoke((MethodInvoker)delegate
                            {
                                // Running on the UI thread
                                txtShellOutput.AppendText("OpenFace: " + outputLine + Environment.NewLine);
                            });
                        }
                    });

                    // Check the exit code (0 indicates success)
                    int exitCode = process.ExitCode;
                    Console.WriteLine($"Exit Code: {exitCode}");
                }
                Console.WriteLine("csv created");
                txtShellOutput.AppendText("csv created. deleting extra columns..." + Environment.NewLine);
                txtLog.AppendText("Removing extra columns from CSV..." + Environment.NewLine);

                string csvFile = csvDir + "\\" + Path.GetFileNameWithoutExtension(txtInputVideo.Text) + ".csv";
                string inputFilePath = csvFile;
                string outputFilePath = csvFile + ".txt";

                using (var reader = new StreamReader(inputFilePath))
                using (var writer = new StreamWriter(outputFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue; // Skip empty lines
                        }

                        string[] fields = line.Split(',');

                        // todo: simplify to just 1 statement?

                        // Filter out the columns to keep
                        var filteredFields = fields
                            .Select((field, index) => new { Field = field, Index = index })
                            .Where(item => item.Index < 5 || item.Index > 298)
                            .Select(item => item.Field)
                            .ToArray();

                        // Filter out the columns to keep
                        var filteredFields2 = filteredFields
                            .Select((field, index) => new { Field = field, Index = index })
                            .Where(item => item.Index < 141 || item.Index > 420)
                            .Select(item => item.Field)
                            .ToArray();

                        string modifiedLine2 = string.Join(",", filteredFields2);

                        modifiedLine2 = modifiedLine2.Replace(",  ", ", ");
                        writer.WriteLine(modifiedLine2);
                    }
                }

                File.Delete(csvFile);
                File.Move(outputFilePath, csvFile);

                Console.WriteLine("Columns removed successfully.");
                txtShellOutput.AppendText("Columns removed successfully. Starting DINet" + Environment.NewLine);
                
                //////////////////////////////////////////////////////////////////////////////
                // starting DINet

                // Define the Conda environment name and the Python script to execute
                string audioFile = txtInputAudio.Text;
                if(!audioFile.EndsWith(".wav"))
                {
                    txtLog.AppendText("Input audio not a wav file. converting to wav......" + Environment.NewLine);
                    Console.WriteLine("input audio not a wav file. converting to wav...");
                    txtShellOutput.AppendText("input audio not a wav file. converting to wav..." + Environment.NewLine);
                    audioFile = await ConvertWithFFMPEG(audioFile, "wav");                    
                }

                string videoFile = txtInputVideo.Text;
                if (!videoFile.EndsWith(".mp4"))
                {
                    txtLog.AppendText("Input video not an mp4 file. converting to mp4..." + Environment.NewLine);
                    Console.WriteLine("input video not an mp4 file. converting to mp4...");
                    txtShellOutput.AppendText("input video not an mp4 file. converting to mp4..." + Environment.NewLine);
                    videoFile = await ConvertWithFFMPEG(videoFile, "mp4");                    
                }

                txtLog.AppendText("Running DINet..." + Environment.NewLine);

                string pythonScriptPath = dinetDir + "\\inference.py";
                string pythonScriptArguments = $"--mouth_region_size=256 --source_video_path=\"{videoFile}\" --source_openface_landmark_path=./asserts/input/{vidFileWithoutExtension}.csv --driving_audio_path=\"{audioFile}\" --pretrained_clip_DINet_path=./asserts/clip_training_DINet_256mouth.pth";

                string scriptDirectory = Path.GetDirectoryName(pythonScriptPath);

                // Construct the command to activate the Conda environment and run the Python script
                command = $"{anacondaScriptsPath}\\activate.bat && activate {condaEnv} && cd {scriptDirectory} && python -u {pythonScriptPath} {pythonScriptArguments}";

                // Create a process start info
                startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    // Start the process
                    process.Start();

                    // Send the command to the process
                    process.StandardInput.WriteLine(command);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    await Task.Run(() =>
                    {
                        // Read and display the output of the process in real-time
                        while (!process.StandardOutput.EndOfStream)
                        {
                            string outputLine = process.StandardOutput.ReadLine();
                            Console.WriteLine("DINet: " + outputLine);

                            txtShellOutput.Invoke((MethodInvoker)delegate
                            {
                                // Running on the UI thread
                                txtShellOutput.AppendText("DINet: " + outputLine + Environment.NewLine);
                            });
                        }
                    });

                    // Check the exit code (0 indicates success)
                    int exitCode = process.ExitCode;
                    Console.WriteLine($"Exit Code: {exitCode}");
                }

                txtLog.AppendText("DINet complete. Cleaning up files..." + Environment.NewLine);
                // cleanup. delete directories and files that were created            

                string inferenceResultFolder = dinetDir + "\\asserts\\inference_result\\";
                
                Directory.Delete(vidFolderPath, true);
                Directory.Delete(csvDir + "\\" + vidFileWithoutExtension + "_aligned", true);
                File.Delete(csvDir + "\\" + vidFileWithoutExtension + ".avi");
                File.Delete(csvDir + "\\" + vidFileWithoutExtension + ".csv");
                File.Delete(csvDir + "\\" + vidFileWithoutExtension + ".hog");
                File.Delete(csvDir + "\\" + vidFileWithoutExtension + "_of_details.txt");
                File.Delete(inferenceResultFolder + vidFileWithoutExtension + "_facial_dubbing.mp4");
                File.Delete(inferenceResultFolder + vidFileWithoutExtension + "_synthetic_face.mp4");
                
                txtShellOutput.AppendText("Process Complete");
                txtLog.AppendText("Process Complete");
                Process.Start(inferenceResultFolder + vidFileWithoutExtension + "_facial_dubbing_add_audio.mp4");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong. Check shell output for details.");
                txtShellOutput.AppendText("Message: " + ex.Message + Environment.NewLine);
                txtShellOutput.AppendText("Stack Trace: " + ex.StackTrace + Environment.NewLine);
            }
            finally
            {
                btnGenerate.Enabled = true;
                btnBrowseInputAudio.Enabled = true;
                btnBrowseInputVideo.Enabled = true;
                txtInputAudio.Enabled = true;
                txtInputVideo.Enabled = true;
            }
        }

        private async Task<string> ConvertWithFFMPEG(string inputFile, string format)
        {
            string filenameNoExt = Path.GetFileNameWithoutExtension(inputFile);
            string directory = Path.GetDirectoryName(inputFile);
            string newFileName = directory + "\\" + filenameNoExt + "." + format;
            string imageParams = "";
            if (inputFile.EndsWith(".jpg") || inputFile.EndsWith(".jpeg") || inputFile.EndsWith(".png"))
                imageParams = " -vcodec libx264 -vf \"pad=ceil(iw/2)*2:ceil(ih/2)*2\" -r 24 -y -an";

            string parameters = $"-i \"{inputFile}\"{imageParams} \"{newFileName}\"";

            string command = $"ffmpeg {parameters}";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                process.Start();

                process.StandardInput.WriteLine(command);
                process.StandardInput.Flush();
                process.StandardInput.Close();

                await Task.Run(() =>
                {
                    // Read and display the output of the process in real-time
                    while (!process.StandardOutput.EndOfStream)
                    {
                        string outputLine = process.StandardOutput.ReadLine();
                        Console.WriteLine("ffmpeg: " + outputLine);

                        txtShellOutput.Invoke((MethodInvoker)delegate
                        {
                            // Running on the UI thread
                            txtShellOutput.AppendText("ffmpeg: " + outputLine + Environment.NewLine);
                        });
                    }
                });

                // Check the exit code (0 indicates success)
                int exitCode = process.ExitCode;
                Console.WriteLine($"Exit Code: {exitCode}");
            }

            return newFileName;
        }
    }
}