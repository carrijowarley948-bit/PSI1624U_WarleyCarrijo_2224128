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
        private static bool isPlaying = false;

        // Tocar música
        public static void PlayMusic()
        {
            try
            {
                // Se já está a tocar, não faz nada
                if (isPlaying && waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                    return;

                StopMusic(); // Limpar recursos antigos

                string musicPath = EncontrarFicheiroMusica();

                if (musicPath != null && File.Exists(musicPath))
                {
                    waveOut = new WaveOutEvent();
                    audioFile = new AudioFileReader(musicPath);
                    waveOut.Init(audioFile);
                    waveOut.Play();
                    isPlaying = true;
                }
                else
                {
                    MessageBox.Show($"Ficheiro MP3 não encontrado!\n\n" +
                                  $"Pastas verificadas:\n" +
                                  $"1. {Path.Combine(Application.StartupPath, "Grégoire_Lourme_-_The_Dark_Protector.mp3")}\n" +
                                  $"2. {Path.Combine(Application.StartupPath, "..", "..", "Grégoire_Lourme_-_The_Dark_Protector.mp3")}\n\n" +
                                  $"Por favor, copie o ficheiro para a pasta:\n{Application.StartupPath}",
                                  "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reproduzir música: {ex.Message}",
                              "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Parar música
        public static void StopMusic()
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                    waveOut.Stop();

                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            isPlaying = false;
        }

        // Verificar se a música está a tocar
        public static bool IsPlaying()
        {
            return isPlaying && waveOut != null && waveOut.PlaybackState == PlaybackState.Playing;
        }

        // Encontrar ficheiro de música
        private static string EncontrarFicheiroMusica()
        {
            string[] possiveisCaminhos = new string[]
            {
                Path.Combine(Application.StartupPath, "Grégoire_Lourme_-_The_Dark_Protector.mp3"),
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", "Grégoire_Lourme_-_The_Dark_Protector.mp3")),
                @"C:\Users\2224128\OneDrive - Escola Digital\Projeto12º\jogo\Grégoire_Lourme_-_The_Dark_Protector.mp3"
            };

            foreach (string caminho in possiveisCaminhos)
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