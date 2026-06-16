using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace jogo
{
    public static class MusicManager
    {
        private static WaveOutEvent waveOut;
        private static AudioFileReader audioFile;
        private static bool tocando = false;

        public static void TocarMusica(string FicheiroMusica)
        {
            try
            {
                if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                {
                    PararMusica();
                }

                string musicPath = EncontrarFicheiroMusica(FicheiroMusica);

                if (musicPath != null && File.Exists(musicPath))
                {
                    waveOut = new WaveOutEvent();

                    audioFile = new AudioFileReader(musicPath);

                    waveOut.Init(audioFile);

                    waveOut.Play();

                    tocando = true;

                    System.Diagnostics.Debug.WriteLine($"A tocar: {Path.GetFileName(musicPath)}");
                }
                else
                {
                    MessageBox.Show($"Ficheiro '{FicheiroMusica}' não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reproduzir música: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PararMusica()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            tocando = false;
        }

        public static bool Ocupado()
        {
            return tocando && waveOut != null && waveOut.PlaybackState == PlaybackState.Playing;
        }

        private static string EncontrarFicheiroMusica(string musicFileName)
        {
            string[] caminhos =
            {
                Path.Combine(Application.StartupPath, musicFileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, musicFileName),
                Path.Combine(Application.StartupPath, "Musicas", musicFileName)
            };

            foreach (string caminho in caminhos)
            {
                if (File.Exists(caminho))
                {
                    return caminho;
                }
            }

            return null;
        }
    }
}