
namespace StrukturData;

internal class Program
{
    static void Main(string[] args)
    {
        List<MahasiswaModel> listMahasiswa = [];
        int menu = 0;

        do
        {
            Console.WriteLine("---=== UJIKOM Praktek LPS Widyatama ===---");
            Console.WriteLine("Menu ::");
            Console.WriteLine("1. Display Data Mahasiswa.");
            Console.WriteLine("2. Tambah Data Mahasiswa.");
            Console.WriteLine("3. Hapus Data Mahasiswa.");
            Console.WriteLine("0. Keluar Program.");
            Console.Write("Pilih Menu :: ");

            string inputMenu = Console.ReadLine();
            if (!int.TryParse(inputMenu, out menu))
            {
                menu = 4;
                ShowMessage(ConsoleColor.DarkRed, "Harap masukan angka!");
            }

            switch (menu)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("\n---=== Menu Display Data Mahasiswa ===---");
                    DisplayMahasiswa(listMahasiswa);
                    break;

                case 2:
                    Console.WriteLine("\n---=== Menu Tambah Data Mahasiswa ===---");
                    AddMahasiswa(listMahasiswa);
                    break;

                case 3:
                    Console.WriteLine("\n---=== Menu Hapus Data Mahasiswa ===---");
                    HapusMahasiswa(listMahasiswa);
                    break;

                default:
                    menu = 4;
                    ShowMessage(ConsoleColor.DarkRed, "Harap pilih menu yang sesuai!");
                    break;
            }

            if (menu != 0)
            {
                Console.ReadKey();
                Console.Clear();
            }
        } while (menu != 0);

        Console.WriteLine("Program Selesai...");
    }

    static void DisplayMahasiswa(List<MahasiswaModel> listMahasiswa)
    {
        if (listMahasiswa.Count == 0)
        {
            ShowMessage(ConsoleColor.DarkYellow, "Data Belum Tersedia, Harap Tambah Data Terlebih Dahulu!");
            return;
        }

        Console.WriteLine("| {0,-10} | {1,-25} | {2,-25} | {3,-13} | {4,-20} |", "NPM", "Nama", "Tempat Lahir", "Tanggal Lahir", "Prodi");
        foreach (var mhs in listMahasiswa.ToList())
        {
            Console.WriteLine("| {0,-10} | {1,-25} | {2,-25} | {3,-13} | {4,-20} |", mhs.NPM, mhs.Nama, mhs.TempatLahir, $"{mhs.TanggalLahir:yyyy-MM-dd}", mhs.Prodi);
        }
    }

    static void AddMahasiswa(List<MahasiswaModel> listMahasiswa)
    {
        MahasiswaModel mahasiswa = new();

        bool npmError = true;
        do
        {
            Console.Write("NPM\t\t:: ");
            mahasiswa.NPM = Console.ReadLine();

            if (listMahasiswa.Any(a => a.NPM.Trim().ToUpper().Equals(mahasiswa.NPM)))
            {
                ShowMessage(ConsoleColor.DarkRed, $"Data Mahasiswa Dengan NPM '{mahasiswa.NPM}' Sudah Ada!");
            }
            else
                npmError = false;

        } while (npmError);

        Console.Write("Nama\t\t:: ");
        mahasiswa.Nama = Console.ReadLine();

        Console.Write("Tempat Lahir\t:: ");
        mahasiswa.TempatLahir = Console.ReadLine();

        bool tanggalLahirError = true;
        do
        {

            Console.Write("Tanggal Lahir (yyyy-MM-dd)\t:: ");
            string tanggalLahir = Console.ReadLine();
            if (DateOnly.TryParseExact(tanggalLahir, "yyyy-MM-dd", out DateOnly tanggalLahirResult))
            {
                mahasiswa.TanggalLahir = tanggalLahirResult;
                tanggalLahirError = false;
            }
            else
            {
                ShowMessage(ConsoleColor.DarkRed, "Format Tanggal Salah!");
                Console.WriteLine();
            }
        } while (tanggalLahirError);

        Console.Write("Prodi\t\t:: ");
        mahasiswa.Prodi = Console.ReadLine();

        listMahasiswa.Add(mahasiswa);
        ShowMessage(ConsoleColor.DarkGreen, "Tambah Data Berhasil!");
    }

    static void HapusMahasiswa(List<MahasiswaModel> listMahasiswa)
    {
        if (listMahasiswa.Count == 0)
        {
            ShowMessage(ConsoleColor.DarkYellow, "Data Belum Tersedia, Harap Tambah Data Terlebih Dahulu!");
            return;
        }

        DisplayMahasiswa(listMahasiswa);

        Console.Write("NPM :: ");
        string npm = Console.ReadLine();
        MahasiswaModel mhs = listMahasiswa.Where(a => a.NPM.Trim().Equals(npm)).FirstOrDefault();

        if (mhs == null)
        {
            ShowMessage(ConsoleColor.DarkYellow, $"Data Mahasiswa dengan NPM '{npm}' Tidak Ada!");
            return;
        }

        listMahasiswa.Remove(mhs);
        ShowMessage(ConsoleColor.DarkGreen, "Hapus Data Berhasil!");
        DisplayMahasiswa(listMahasiswa);
    }

    static void ShowMessage(ConsoleColor backgroundColor, string message)
    {
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(message);
        Console.BackgroundColor = ConsoleColor.Black;
    }
}
