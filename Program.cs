using System;

namespace SistemPenggajian
{
    public abstract class Pegawai
    {
        protected string NamaLengkap { get; }
        protected string NomorIdentitas { get; }
        protected double UpahDasar { get; }

        protected Pegawai(string nama, string id, double upahDasar)
        {
            NamaLengkap = nama;
            NomorIdentitas = id;
            UpahDasar = upahDasar;
        }

        public string AmbilNama() => NamaLengkap;
        public string AmbilID() => NomorIdentitas;
        public double AmbilUpahDasar() => UpahDasar;

        public abstract double KalkulasiGaji();
    }

    public class PegawaiTetap : Pegawai
    {
        private const double BonusTetap = 500000;

        public PegawaiTetap(string nama, string id, double upahDasar)
            : base(nama, id, upahDasar) { }

        public override double KalkulasiGaji()
        {
            return AmbilUpahDasar() + BonusTetap;
        }
    }

    public class PegawaiKontrak : Pegawai
    {
        private const double PotonganKontrak = 200000;

        public PegawaiKontrak(string nama, string id, double upahDasar)
            : base(nama, id, upahDasar) { }

        public override double KalkulasiGaji()
        {
            return AmbilUpahDasar() - PotonganKontrak;
        }
    }

    public class PesertaMagang : Pegawai
    {
        public PesertaMagang(string nama, string id, double upahDasar)
            : base(nama, id, upahDasar) { }

        public override double KalkulasiGaji()
        {
            return AmbilUpahDasar();
        }
    }

    public class ProgramUtama
    {
        public static void Jalankan()
        {
            while (true)
            {
                TampilkanMenuUtama();
                string opsi = Console.ReadLine();

                if (opsi == "4")
                {
                    Console.WriteLine("Aplikasi ditutup. Sampai jumpa!");
                    return;
                }

                ProsesOpsiPegawai(opsi);
            }
        }

        private static void TampilkanMenuUtama()
        {
            Console.Clear();
            Console.WriteLine("==== Sistem Penggajian Pegawai ====");
            Console.WriteLine("1. Input Data Pegawai Tetap");
            Console.WriteLine("2. Input Data Pegawai Kontrak");
            Console.WriteLine("3. Input Data Peserta Magang");
            Console.WriteLine("4. Keluar dari Aplikasi");
            Console.Write("Silakan pilih menu (1-4): ");
        }

        private static void ProsesOpsiPegawai(string opsi)
        {
            Console.WriteLine();
            Console.Write("Masukkan Nama Lengkap: ");
            string nama = Console.ReadLine();
            Console.Write("Masukkan Nomor ID: ");
            string id = Console.ReadLine();

            double upah;
            while (true)
            {
                Console.Write("Masukkan Upah Dasar: Rp.");
                if (double.TryParse(Console.ReadLine(), out upah)) break;
                Console.WriteLine("Input harus berupa angka. Silakan coba lagi.");
            }

            Pegawai staff = BuatPegawaiBerdasarkanOpsi(opsi, nama, id, upah);

            if (staff != null)
            {
                TampilkanDetailGaji(staff);
            }
            else
            {
                Console.WriteLine("Pilihan tidak dikenali! Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }

        private static Pegawai BuatPegawaiBerdasarkanOpsi(string opsi, string nama, string id, double upah)
        {
            switch (opsi)
            {
                case "1": return new PegawaiTetap(nama, id, upah);
                case "2": return new PegawaiKontrak(nama, id, upah);
                case "3": return new PesertaMagang(nama, id, upah);
                default: return null;
            }
        }

        private static void TampilkanDetailGaji(Pegawai staff)
        {
            Console.WriteLine();
            Console.WriteLine($"Detail Gaji untuk {staff.AmbilNama()}:");
            Console.WriteLine($"Nomor ID: {staff.AmbilID()}");
            Console.WriteLine($"Upah Dasar: Rp.{staff.AmbilUpahDasar()}");
            Console.WriteLine($"Total Gaji: Rp.{staff.KalkulasiGaji()}");
            Console.WriteLine("\nTekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProgramUtama.Jalankan();
        }
    }
}