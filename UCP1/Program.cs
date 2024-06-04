using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace UCP1PABD
{
    internal class Program
    {
        // Definisi struct untuk menyimpan data admin
        struct Users
        {
            public string username;
            public string password;

            public Users(string username, string password)
            {
                this.username = username;
                this.password = password;
            }
        }

        // Metode utama program
        static void Main(string[] args)
        {
            Console.WriteLine("** Login **"); // Menampilkan pesan "Login" ke layar console
            Program pr = new Program();

            bool successful = false;
            // Looping untuk login
            while (!successful)
            {
                Console.WriteLine("\nMasukkan Username: "); // Menampilkan pesan untuk meminta input username
                var username = Console.ReadLine(); // Membaca input username dari user
                Console.WriteLine("\nMasukkan Password: ");
                var password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    // Menampilkan pesan kesalahan jika username atau password kosong
                    Console.WriteLine("\nUsername atau password tidak boleh kosong, silahkan coba lagi\n");
                    Console.WriteLine("----------------------------------------------------");
                }
                else if (pr.ValidasiUser(username, password)) // Memeriksa validasi user dari database dengan memanggil metode Validasi User
                {
                    Console.WriteLine("\nLogin sukses"); // Menampilkan pesan login sukses
                    successful = true;
                }
                else
                {
                    // Menampilkan pesan kesalahan jika username atau password salah
                    Console.WriteLine("\nUsername atau password salah, silahkan coba lagi\n");
                    Console.WriteLine("----------------------------------------------------");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("\nKetik K untuk Terhubung ke Database atau E untuk keluar dari Aplikasi: ");
                    char chr = Convert.ToChar(Console.ReadLine()); //membaca input dari pengguna
                    switch (chr)
                    {
                        case 'K':
                            {
                                Console.Clear(); // Membersihkan layar console
                                Console.WriteLine("Masukkan Database yang dituju kemudian klik enter: ");
                                string db = Console.ReadLine(); // Membaca input dari penguna yang berupa nama db dan menyimpannya d variabl db

                                // String koneksi untuk terhubung ke database SQL Server
                                string strKoneksi = $"Data source = LAPTOP-FJJPQU6D\\ANGGI_PUSPITA; initial catalog = {db}; User ID = sa; password = anggi2022";
                                // Membuat koneksi ke database
                                using (SqlConnection conn = new SqlConnection(strKoneksi))
                                {
                                    conn.Open();  // Membuka koneksi ke database
                                    Console.Clear();
                                    while (true)
                                    {
                                        try
                                        {
                                            // Menampilkan menu pilihan
                                            Console.WriteLine("--- MENU ---\n");
                                            Console.WriteLine("1. Data Masyarakat");
                                            Console.WriteLine("2. Data Admin");
                                            Console.WriteLine("3. Data dokumen");
                                            Console.WriteLine("4. Keluar");
                                            Console.Write("\nEnter your choice (1-4): ");
                                            char ch = Convert.ToChar(Console.ReadLine()); // Membaca pilihan menu dari user

                                            switch (ch)
                                            {
                                                case '1':
                                                    {
                                                        Console.Clear();
                                                        // Menampilkan menu pilihan
                                                        Console.WriteLine("--- Data Masyarakat ---\n");
                                                        Console.WriteLine("1. Tambah data penduduk");
                                                        Console.WriteLine("2. Melihat data penduduk");
                                                        Console.WriteLine("3. Hapus data penduduk");
                                                        Console.WriteLine("4. Update data penduduk");
                                                        Console.WriteLine("5. Keluar");
                                                        Console.Write("\nEnter your choice (1-5):");
                                                        char ch1 = Convert.ToChar(Console.ReadLine());

                                                        switch (ch1)
                                                        {
                                                            case '1':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Tambah data penduduk ---\n");
                                                                    // Meminta input data dari user
                                                                    Console.WriteLine("\nMasukkan ID penduduk: ");
                                                                    string Id_Penduduk = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan NIK:");
                                                                    string NIK = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Nama:");
                                                                    string Nama = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Dusun:");
                                                                    string Dusun = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Desa:");
                                                                    string Desa = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Kecamatan:");
                                                                    string Kecamatan = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Kabupaten:");
                                                                    string Kabupaten = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Jenis Kelamin (L/P):");
                                                                    string Jenis_Kelamin = Console.ReadLine().ToUpper(); // ubah menjadi huruf besar untuk mempermudah validasi
                                                                    Console.WriteLine("\nMasukkan Agama:");
                                                                    string Agama = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan No telepon:");
                                                                    string No_Telepon = Console.ReadLine();

                                                                    if (string.IsNullOrEmpty(Id_Penduduk) || string.IsNullOrEmpty(NIK) || string.IsNullOrEmpty(Nama) || string.IsNullOrEmpty(Dusun) ||
                                                                        string.IsNullOrEmpty(Desa) || string.IsNullOrEmpty(Kecamatan) || string.IsNullOrEmpty(Kabupaten) || string.IsNullOrEmpty(Jenis_Kelamin)
                                                                        || string.IsNullOrEmpty(Agama) || string.IsNullOrEmpty(No_Telepon))
                                                                    {
                                                                        Console.WriteLine("\nData tidak boleh kosong. \nData gagal disimpan");
                                                                    }
                                                                    else if (!IsValidNama(Nama))
                                                                    {
                                                                        Console.WriteLine("\nNama hanya boleh terdiri dari huruf. \nData gagal disimpan");
                                                                    }
                                                                    else if (!IsNumeric(Id_Penduduk) || !IsNumeric(NIK) || !IsAlphabetic(Desa) || !IsAlphabetic(Kecamatan) || !IsAlphabetic(Kabupaten) ||
                                                                        !IsGenderValid(Jenis_Kelamin) || !IsAlphabetic(Agama) || !IsNumeric(No_Telepon))
                                                                    {
                                                                        Console.WriteLine("\nPastikan Anda memasukkan data sesuai format yang diminta. \nData gagal disimpan");
                                                                    }
                                                                    else
                                                                    {
                                                                        try
                                                                        {
                                                                            // Memeriksa apakah ID penduduk dan NIK sudah ada dalam database
                                                                            if (Validasiduplikasidata(Id_Penduduk, NIK, conn))
                                                                            {
                                                                                Console.WriteLine("\nData ID Penduduk dan NIK sudah ada digunakan. Tambahkan data lain.");
                                                                            }
                                                                            else
                                                                            {
                                                                                // Memanggil method insert untuk menambah data penduduk ke database
                                                                                pr.insert(Id_Penduduk, NIK, Nama, $"{Dusun}, {Desa}, {Kecamatan}, {Kabupaten}", Jenis_Kelamin, Agama, No_Telepon, conn);
                                                                            }
                                                                        }
                                                                        catch
                                                                        {
                                                                            // Menangani exception/pengecualian jika user tidak memiliki akses untuk menambah data
                                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                            case '2':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Data Penduduk ---");
                                                                    Console.WriteLine();
                                                                    // Memanggil method baca untuk membaca dan menampilkan data penduduk dari database
                                                                    pr.baca(conn);
                                                                }
                                                                break;
                                                            case '3':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("\n --- Hapus Data ---\n");
                                                                    Console.WriteLine("Masukkan Nama penduduk yang ingin dihapus: ");
                                                                    string Nama = Console.ReadLine(); // Membaca Nama dari user
                                                                    Console.WriteLine("\nMasukkan ID penduduk yang ingin dihapus: ");
                                                                    string Id_Penduduk = Console.ReadLine();

                                                                    if (string.IsNullOrEmpty(Nama) || string.IsNullOrEmpty(Id_Penduduk))
                                                                    {
                                                                        Console.WriteLine("\nNama dan ID Penduduk tidak boleh kosong.");
                                                                    }
                                                                    else if (!Validasiinput(Nama)) // Memeriksa apakah Nama hanya berisi huruf
                                                                    {
                                                                        Console.WriteLine("\nNama harus berisi huruf saja.");
                                                                    }
                                                                    else
                                                                    {
                                                                        try
                                                                        {
                                                                            if (pr.DataExists(Nama, Id_Penduduk, conn))
                                                                            {
                                                                                if (conn.State != ConnectionState.Open)
                                                                                {
                                                                                    conn.Open();
                                                                                }

                                                                                // Memanggil method delete untuk menghapus data berdasarkan Nama dan ID Penduduk
                                                                                pr.delete(Nama, Id_Penduduk, conn);
                                                                                Console.WriteLine("\nData penduduk dengan Nama {0} dan ID Penduduk {1} telah berhasil dihapus.", Nama, Id_Penduduk);
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("\nData penduduk dengan Nama {0} dan ID Penduduk {1} tidak ditemukan dalam database.", Nama, Id_Penduduk);
                                                                            }
                                                                        }
                                                                        catch (Exception e)
                                                                        {
                                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menghapus data atau terjadi kesalahan saat menghapus data.");
                                                                            Console.WriteLine(e.ToString());
                                                                        }
                                                                        finally
                                                                        {
                                                                            if (conn.State != ConnectionState.Closed)
                                                                            {
                                                                                conn.Close();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                            case '4':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Update data penduduk ---\n");
                                                                    Console.WriteLine("Masukkan Nama penduduk yang ingin diupdate:\n");
                                                                    string NamaUpdate = Console.ReadLine(); // Membaca Nama penduduk yang akan diupdate dari user

                                                                    if (string.IsNullOrEmpty(NamaUpdate))
                                                                    {
                                                                        Console.WriteLine("\nNama tidak boleh kosong.");
                                                                    }
                                                                    else
                                                                    {
                                                                        // Memeriksa apakah Nama yang ingin diupdate ada dalam database
                                                                        if (ValidasiNama(NamaUpdate, conn))
                                                                        {
                                                                            // Jika Nama ditemukan, tampilkan opsi update
                                                                            Console.WriteLine("\nPilih data yang ingin diupdate:");
                                                                            Console.WriteLine("1. Nama");
                                                                            Console.WriteLine("2. Alamat");
                                                                            Console.WriteLine("3. Jenis Kelamin");
                                                                            Console.WriteLine("4. Agama");
                                                                            Console.WriteLine("5. No Telepon");
                                                                            Console.WriteLine("6. Kembali ke menu utama");
                                                                            Console.Write("\nEnter your choice (1-6):");
                                                                            char ch2 = Convert.ToChar(Console.ReadLine());

                                                                            switch (ch2)
                                                                            {
                                                                                case '1':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Nama baru:");
                                                                                        string NamaBaru = Console.ReadLine();
                                                                                        if (!IsAlphabetic(NamaBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nNama hanya boleh terdiri dari huruf. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateNama(NamaUpdate, NamaBaru, conn);
                                                                                        Console.WriteLine("\nNama telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '2':
                                                                                    {
                                                                                        Console.WriteLine("\n-- Masukkan Alamat baru --");
                                                                                        Console.WriteLine("\nMasukkan Dusun:");
                                                                                        string DusunBaru = Console.ReadLine();
                                                                                        Console.WriteLine("\nMasukkan Desa:");
                                                                                        string DesaBaru = Console.ReadLine();
                                                                                        Console.WriteLine("\nMasukkan Kecamatan:");
                                                                                        string KecamatanBaru = Console.ReadLine();
                                                                                        Console.WriteLine("\nMasukkan Kabupaten:");
                                                                                        string KabupatenBaru = Console.ReadLine();

                                                                                        if (!IsAlphabetic(DusunBaru) || !IsAlphabetic(DesaBaru) || !IsAlphabetic(KecamatanBaru) || !IsAlphabetic(KabupatenBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nDesa, Kecamatan, dan Kabupaten hanya boleh terdiri dari huruf. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateAlamat(NamaUpdate, DusunBaru, DesaBaru, KecamatanBaru, KabupatenBaru, conn);
                                                                                        Console.WriteLine("\nAlamat telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '3':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Jenis Kelamin baru (L/P):");
                                                                                        string JKBaru = Console.ReadLine();
                                                                                        if (!IsGenderValid(JKBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nJenis Kelamin hanya bisa L atau P. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateJenisKelamin(NamaUpdate, JKBaru, conn);
                                                                                        Console.WriteLine("\nJenis Kelamin telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '4':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Agama baru:");
                                                                                        string AgamaBaru = Console.ReadLine();
                                                                                        if (!IsAlphabetic(AgamaBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nAgama hanya boleh terdiri dari huruf. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateAgama(NamaUpdate, AgamaBaru, conn);
                                                                                        Console.WriteLine("\nAgama telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '5':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan No Telepon baru:");
                                                                                        string NoTeleponBaru = Console.ReadLine();
                                                                                        if (!IsNumeric(NoTeleponBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nNo Telepon hanya boleh terdiri dari angka. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateNoTelepon(NamaUpdate, NoTeleponBaru, conn);
                                                                                        Console.WriteLine("\nNo Telepon telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '6':
                                                                                    break;
                                                                                default:
                                                                                    Console.WriteLine("\nInvalid option");
                                                                                    break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("\nNama yang Anda masukkan tidak ditemukan di database.");
                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                            case '5':
                                                                conn.Close(); // Menutup koneksi ke database
                                                                Console.Clear(); // Membersihkan layar console
                                                                Main(new string[0]); // Memanggil method Main untuk kembali ke awal program
                                                                return; // Menghentikan eksekusi method saat ini
                                                            default:
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("\nInvalid option");
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case '2':
                                                    {
                                                        Console.Clear();
                                                        // Menampilkan menu pilihan
                                                        Console.WriteLine("--- Data Admin ---\n ");
                                                        Console.WriteLine("1. Tambah Data Admin");
                                                        Console.WriteLine("2. Lihat data admin");
                                                        Console.WriteLine("3. Hapus data admin");
                                                        Console.WriteLine("4. Update data admin");
                                                        Console.WriteLine("5. Keluar");
                                                        Console.Write("\nEnter your choice (1-5):");
                                                        char ch2 = Convert.ToChar(Console.ReadLine());

                                                        switch (ch2)
                                                        {
                                                            case '1':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Tambah Data Admin ---\n");
                                                                    // Meminta input data admin dari user
                                                                    Console.WriteLine("\nMasukkan ID Admin:");
                                                                    string ID_Admin = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Nama:");
                                                                    string Nama = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Username:");
                                                                    string Username = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Password:");
                                                                    string Password = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan No Telepon:");
                                                                    string No_Telepon = Console.ReadLine();

                                                                    // Memeriksa apakah ada input yang kosong
                                                                    if (string.IsNullOrEmpty(ID_Admin) || string.IsNullOrEmpty(Nama) || string.IsNullOrEmpty(Username)
                                                                        || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(No_Telepon))
                                                                    {
                                                                        Console.WriteLine("\nSemua input harus diisi. Data gagal disimpan.");
                                                                    }
                                                                    else
                                                                    {
                                                                        try
                                                                        {
                                                                            // Memanggil method insertAdmin untuk menambah data admin ke database
                                                                            pr.insertAdmin(ID_Admin, Nama, Username, Password, No_Telepon, conn);
                                                                        }
                                                                        catch
                                                                        {
                                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                                        }
                                                                    }

                                                                }
                                                                break;
                                                            case '2':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Data Admin ---");
                                                                    Console.WriteLine();
                                                                    // Memanggil method read untuk membaca dan menampilkan data admin dari database
                                                                    pr.read(conn);
                                                                }
                                                                 break;
                                                            case '3':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Hapus Data Admin ---");
                                                                    Console.WriteLine("\nMasukkan ID Admin yang ingin dihapus:");
                                                                    string ID_Admin = Console.ReadLine();
                                                                    pr.deleteAdmin(ID_Admin, conn);
                                                                }
                                                                break;
                                                            case '4':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Update Data Admin ---");
                                                                    Console.WriteLine("\nMasukkan ID Admin yang ingin diperbarui:");
                                                                    string ID_AdminUpdate = Console.ReadLine();

                                                                    if (string.IsNullOrEmpty(ID_AdminUpdate))
                                                                    {
                                                                        Console.WriteLine("\nID Admin tidak boleh kosong.");
                                                                    }
                                                                    else
                                                                    {
                                                                        // Memeriksa apakah ID Admin yang ingin diperbarui ada dalam database
                                                                        if (ValidasiIDAdmin(ID_AdminUpdate, conn))
                                                                        {
                                                                            // Jika ID Admin ditemukan, tampilkan opsi update
                                                                            Console.WriteLine("\nPilih data yang ingin diupdate:");
                                                                            Console.WriteLine("1. Nama");
                                                                            Console.WriteLine("2. Username");
                                                                            Console.WriteLine("3. Password");
                                                                            Console.WriteLine("4. No Telepon");
                                                                            Console.WriteLine("5. Kembali ke menu utama");
                                                                            Console.Write("\nEnter your choice (1-5):");
                                                                            char ch4 = Convert.ToChar(Console.ReadLine());

                                                                            switch (ch4)
                                                                            {
                                                                                case '1':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Nama baru:");
                                                                                        string NamaBaru = Console.ReadLine();
                                                                                        if (!IsAlphabetic(NamaBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nNama hanya boleh terdiri dari huruf. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateNamaAdmin(ID_AdminUpdate, NamaBaru, conn);
                                                                                        Console.WriteLine("\nNama telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '2':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Username baru:");
                                                                                        string UsernameBaru = Console.ReadLine();
                                                                                        pr.updateUsernameAdmin(ID_AdminUpdate, UsernameBaru, conn);
                                                                                        Console.WriteLine("\nUsername telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '3':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan Password baru:");
                                                                                        string PasswordBaru = Console.ReadLine();
                                                                                        pr.updatePasswordAdmin(ID_AdminUpdate, PasswordBaru, conn);
                                                                                        Console.WriteLine("\nPassword telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '4':
                                                                                    {
                                                                                        Console.WriteLine("\nMasukkan No Telepon baru:");
                                                                                        string NoTeleponBaru = Console.ReadLine();
                                                                                        if (!IsNumeric(NoTeleponBaru))
                                                                                        {
                                                                                            Console.WriteLine("\nNo Telepon hanya boleh terdiri dari angka. \nData gagal diupdate");
                                                                                            break;
                                                                                        }
                                                                                        pr.updateNoTeleponAdmin(ID_AdminUpdate, NoTeleponBaru, conn);
                                                                                        Console.WriteLine("\nNo Telepon telah berhasil diupdate.");
                                                                                    }
                                                                                    break;
                                                                                case '5':
                                                                                    {
                                                                                        // Kembali ke menu utama
                                                                                    }
                                                                                    break;
                                                                                default:
                                                                                    Console.WriteLine("\nInvalid option");
                                                                                    break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("\nID Admin yang Anda masukkan tidak ditemukan di database.");
                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                            case '5':
                                                                conn.Close();
                                                                Console.Clear();
                                                                Main(new string[0]);
                                                                return;
                                                            default:
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("\nInvalid option");
                                                                }
                                                                break;
                                                        
                                                        }
                                                    }
                                                    break;
                                                case '3':
                                                    {
                                                        Console.Clear();
                                                        // Menampilkan menu pilihan
                                                        Console.WriteLine("--- Data Dokumen ---\n ");
                                                        Console.WriteLine("1. Tambah Data Dokumen");
                                                        Console.WriteLine("2. Lihat data dokumen");
                                                        Console.WriteLine("3. Keluar");
                                                        Console.Write("\nEnter your choice (1-3):");
                                                        char ch3 = Convert.ToChar(Console.ReadLine());

                                                        switch (ch3)
                                                        {
                                                            case '1':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Tambah Dokumen ---\n");
                                                                    // Meminta input data dokumen dari user
                                                                    Console.WriteLine("\nMasukkan Jenis Dokumen:");
                                                                    string Jenis_Dokumen = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan Tanggal Terbit:");
                                                                    string Tanggal_Terbit = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan ID Admin:");
                                                                    string ID_admin = Console.ReadLine();
                                                                    Console.WriteLine("\nMasukkan ID Penduduk:");
                                                                    string ID_Penduduk = Console.ReadLine();
                                                                    try
                                                                    {
                                                                        // Memanggil method insertDokumen untuk mengisi data dokumen ke database
                                                                        pr.insertDokumen(Jenis_Dokumen, Tanggal_Terbit, ID_admin, ID_Penduduk, conn);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        // Menangani exception jika terjadi kesalahan saat mengisi data dokumen
                                                                        Console.WriteLine($"\nTerjadi kesalahan: {ex.Message}");
                                                                    }

                                                                }
                                                                break;
                                                            case '2':
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("--- Data Dokumen ---");
                                                                    Console.WriteLine();
                                                                    // Memanggil method read untuk membaca dan menampilkan data admin dari database
                                                                    pr.lihat(conn);
                                                                }
                                                                break;
                                                            case '3':
                                                                conn.Close();
                                                                Console.Clear();
                                                                Main(new string[0]);
                                                                return;
                                                            default:
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("\nInvalid option");
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case '4':
                                                    conn.Close();
                                                    Console.Clear();
                                                    Main(new string[0]);
                                                    return;
                                                default:
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("\nInvalid option");
                                                    }
                                                    break;

                                            }
                                        }
                                        catch
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\nCheck for the value entered .");
                                        }
                                    }
                                }
                            }
                        case 'E':
                            return; // Menghentikan eksekusi method saat ini dan kembali ke method pemanggil
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red; // Mengubah warna teks menjadi merah
                    Console.WriteLine("Tidak dapat mengakses database tersebut\n"); // Menampilkan pesan kesalahan
                    Console.ResetColor(); // Mereset warna teks ke default
                    Console.WriteLine("Error: " + ex.Message); // Menampilkan pesan kesalahan dari excepti
                }
            }
        }

        public void baca(SqlConnection con)
        {
            // Menampilkan header tabel
            Console.WriteLine("+------------------+----------------+----------------+-----------------------------------+---------------+----------+---------------+");
            Console.WriteLine("|   ID Penduduk    |     NIK        |      Nama      |              Alamat               | Jenis Kelamin |  Agama   |  No Telepon   |");
            Console.WriteLine("+------------------+----------------+----------------+-----------------------------------+---------------+----------+---------------+");

            // Membuat perintah SQL untuk mengambil data dari tabel Data_Penduduk
            SqlCommand cmd = new SqlCommand("Select ID_Penduduk, NIK, Nama, Alamat, Jenis_kelamin, Agama, No_Telepon from Data_Penduduk", con);
            // Membuka koneksi dan mendapatkan data menggunakan SqlDataReader
            SqlDataReader r = cmd.ExecuteReader();

            // Membaca data per baris dan menampilkannya di console
            while (r.Read())
            {
                // Mengambil nilai dari setiap kolom dan mengkonversinya menjadi string
                string Id_Penduduk = r["ID_Penduduk"].ToString();
                string NIK = r["NIK"].ToString();
                string Nama = r["Nama"].ToString();
                string Alamat = r["Alamat"].ToString();
                string Jenis_kelamin = r["Jenis_kelamin"].ToString();
                string Agama = r["Agama"].ToString();
                string No_Telepon = r["No_Telepon"].ToString();

                // Menampilkan data dalam format yang rapi
                Console.WriteLine($"|  {Id_Penduduk,-12}   | {NIK,-12} | {Nama,-14} | {Alamat,-32}  | {Jenis_kelamin,-13} | {Agama,-8} | {No_Telepon,-12} |");
            }
            // Menampilkan footer tabel
            Console.WriteLine("+------------------+----------------+----------------+-----------------------------------+---------------+----------+---------------+");
            r.Close(); // Menutup SqlDataReader
        }

        public void insert(string Id_Penduduk, string NIK, string Nama, string Alamat, string Jenis_Kelamin, string Agama, string No_Telepon, SqlConnection con)
        {
            // Query SQL untuk menambahkan data ke tabel Data_Penduduk
            string str = "insert into Data_Penduduk (Id_Penduduk, NIK, Nama, Alamat, Jenis_kelamin, Agama, No_Telepon) " + "values (@id_pddk,@NIK, @Nama, @Alamat, @Jk, @Agama, @No_Telepon)";
            SqlCommand cmd = new SqlCommand(str, con); // Membuat objek SqlCommand dengan query dan koneksi yang diberikan
            cmd.CommandType = CommandType.Text;

            // Mengatur parameter untuk query SQL dengan nilai yang diberikan
            cmd.Parameters.AddWithValue("@id_pddk", Id_Penduduk);
            cmd.Parameters.AddWithValue("@NIK", NIK);
            cmd.Parameters.AddWithValue("@Nama", Nama);
            cmd.Parameters.AddWithValue("@Alamat", Alamat);
            cmd.Parameters.AddWithValue("@Jk", Jenis_Kelamin);
            cmd.Parameters.AddWithValue("@Agama", Agama);
            cmd.Parameters.AddWithValue("@No_Telepon", No_Telepon);
            cmd.ExecuteNonQuery(); // Menjalankan perintah SQL untuk menambahkan data
            Console.WriteLine("\nData Berhasil Ditambahkan");
        }

        // Metode untuk memeriksa apakah data dengan NIK dan id penduduk yang sama sudah ada dalam database
        static bool Validasiduplikasidata(string Id_Penduduk, string NIK,  SqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM Data_Penduduk WHERE Id_Penduduk = @id_pddk OR NIK = @NIK ";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_pddk", Id_Penduduk);
                cmd.Parameters.AddWithValue("@NIK", NIK);
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Mengembalikan true jika data sudah ada, dan false jika belum
            }
        }
        // Validasi untuk memeriksa apakah input hanya terdiri dari huruf
        public static bool IsAlphabetic(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }

        // Validasi untuk memeriksa apakah input hanya terdiri dari angka
        public static bool IsNumeric(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit);
        }

        // Validasi untuk memeriksa apakah input jenis kelamin hanya L atau P
        public static bool IsGenderValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && (input.ToUpper() == "L" || input.ToUpper() == "P");
        }

        public void delete(string Nama, string Id_Penduduk, SqlConnection con)
        {
            // Validasi apakah Nama hanya berisi huruf
            if (!IsAllLetters(Nama))
            {
                Console.WriteLine("\nNama hanya boleh berisi huruf.");
                return;
            }

            // Validasi apakah ID penduduk hanya berisi angka
            if (!IsAllDigits(Id_Penduduk))
            {
                Console.WriteLine("\nID Penduduk hanya boleh berisi angka.");
                return;
            }

            // Query SQL untuk memeriksa apakah data ada dalam tabel Data_Penduduk
            string checkIfExistsQuery = "SELECT COUNT(*) FROM Data_Penduduk WHERE Nama = @Nama AND Id_Penduduk = @id_pddk";
            SqlCommand checkIfExistsCmd = new SqlCommand(checkIfExistsQuery, con);
            checkIfExistsCmd.Parameters.AddWithValue("@Nama", Nama);
            checkIfExistsCmd.Parameters.AddWithValue("@id_pddk", Id_Penduduk);

            int rowCount = (int)checkIfExistsCmd.ExecuteScalar();

            // Jika data tidak ditemukan, tampilkan pesan dan keluar dari metode
            if (rowCount == 0)
            {
                Console.WriteLine("\nData tidak ditemukan dalam tabel Data_Penduduk.");
                return;
            }

            // Jika data ditemukan, jalankan perintah DELETE untuk menghapus data
            string deleteQuery = "DELETE FROM Data_Penduduk WHERE Nama = @Nama AND Id_Penduduk = @id_pddk";
            SqlCommand cmd = new SqlCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@Nama", Nama);
            cmd.Parameters.AddWithValue("@id_pddk", Id_Penduduk);
            cmd.ExecuteNonQuery(); // Menjalankan perintah SQL untuk menghapus data
            Console.WriteLine("\nData berhasil dihapus");
        }
        static bool IsValidNama(string nama)
        {
            foreach (char c in nama)
            {
                // Memeriksa apakah setiap karakter dalam nama adalah huruf atau spasi
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }
    
    // Method untuk memeriksa apakah sebuah string hanya berisi huruf
    private bool IsAllLetters(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }

        // Method untuk memeriksa apakah sebuah string hanya berisi angka
        private bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        // Method untuk memeriksa apakah data dengan Nama dan NIK tersebut ada dalam database
        public bool DataExists(string Nama, string Id_Penduduk, SqlConnection conn)
        {
            bool exists = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Data_Penduduk WHERE Nama = @Nama AND Id_Penduduk = @ID_pnddk", conn);
                cmd.Parameters.AddWithValue("@Nama", Nama);
                cmd.Parameters.AddWithValue("@ID_pnddk", Id_Penduduk);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return exists;
        }


        static bool ValidasiNama(string nama, SqlConnection conn)
        {
            // Menggunakan ekspresi reguler untuk memeriksa apakah nama hanya terdiri dari huruf dan spasi
            return Regex.IsMatch(nama, @"^[a-zA-Z ]+$");
        }

        // Metode Validasiinput
        static bool Validasiinput(string input)
        {
            return true;
        }


        public void read(SqlConnection con)
        {
            Console.WriteLine("+--------------+----------------+----------------+----------------+--------------+");
            Console.WriteLine("|   ID_Admin   |      Nama      |    Username    |    Password    |  No Telepon  |");
            Console.WriteLine("+--------------+----------------+----------------+----------------+--------------+");

            SqlCommand cmd = new SqlCommand("Select ID_Admin, Nama, Username, Password, No_Telepon from Data_Admin", con);
            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                string ID_Admin = r["ID_Admin"].ToString();
                string Nama = r["Nama"].ToString();
                string Username = r["Username"].ToString();
                string Password = r["Password"].ToString();
                string No_Telepon = r["No_Telepon"].ToString();

                Console.WriteLine($"|   {ID_Admin,-8}   | {Nama,-14} | {Username,-14} | {Password,-14} | {No_Telepon,-12} |");
            }
            Console.WriteLine("+--------------+----------------+----------------+----------------+--------------+");
            r.Close();
        }

        public void updateNama(string namaLama, string namaBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Penduduk SET Nama = @NamaBaru WHERE Nama = @NamaLama";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NamaBaru", namaBaru);
            cmd.Parameters.AddWithValue("@NamaLama", namaLama);
            cmd.ExecuteNonQuery();
        }

        public void updateAlamat(string Nama, string DusunBaru, string DesaBaru, string KabupatenBaru, string KecamatanBaru, SqlConnection conn)
        {
            try
            {
                // Pastikan koneksi ditutup jika masih terbuka
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                // Buka koneksi baru
                conn.Open();

                // Gunakan koneksi untuk melakukan pembaruan alamat
                string query = "UPDATE Data_Penduduk SET Alamat = @Alamat WHERE Nama = @Nama";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Gabungkan komponen alamat menjadi satu string
                    string alamatBaru = $"{DusunBaru}, {DesaBaru}, {KecamatanBaru}, {KabupatenBaru}";

                    // Tambahkan parameter ke perintah SQL
                    command.Parameters.AddWithValue("@Alamat", alamatBaru);
                    command.Parameters.AddWithValue("@Nama", Nama);

                    // Jalankan perintah SQL
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah berhasil diperbarui.");
                }
            }
            catch (Exception ex)
            {
                // Tangani pengecualian
                Console.WriteLine("Terjadi kesalahan saat memperbarui alamat: " + ex.Message);
            }
            finally
            {
                // Pastikan koneksi ditutup setelah digunakan
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void updateJenisKelamin(string nama, string jenisKelaminBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Penduduk SET Jenis_Kelamin = @JenisKelamin WHERE Nama = @Nama";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@JenisKelamin", jenisKelaminBaru);
            cmd.Parameters.AddWithValue("@Nama", nama);
            cmd.ExecuteNonQuery();
        }

        public void updateAgama(string nama, string agamaBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Penduduk SET Agama = @Agama WHERE Nama = @Nama";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Agama", agamaBaru);
            cmd.Parameters.AddWithValue("@Nama", nama);
            cmd.ExecuteNonQuery();
        }

        public void updateNoTelepon(string nama, string noTeleponBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Penduduk SET No_Telepon = @NoTelepon WHERE Nama = @Nama";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NoTelepon", noTeleponBaru);
            cmd.Parameters.AddWithValue("@Nama", nama);
            cmd.ExecuteNonQuery();
        }

        public void insertAdmin(string ID_Admin, string Nama, string Username, string Password, string No_Telepon, SqlConnection con)
        {
            string str = "insert into Data_Admin (ID_Admin, Nama, Username, Password, No_Telepon) " + "values (@ID_Admin, @Nama, @Username, @Password, @No_Telepon)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID_Admin", ID_Admin);
            cmd.Parameters.AddWithValue("@Nama", Nama);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@No_Telepon", No_Telepon);
            cmd.ExecuteNonQuery();
            Console.WriteLine("\nData Admin Berhasil Ditambahkan");
        }
        public void deleteAdmin(string ID_Admin, SqlConnection conn)
        {
            string query = "DELETE FROM Data_Admin WHERE ID_Admin = @ID_Admin";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID_Admin", ID_Admin);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("\nData admin berhasil dihapus.");
                }
                else
                {
                    Console.WriteLine("\nData admin dengan ID_Admin tersebut tidak ditemukan.");
                }
            }
        }
        // Method untuk validasi ID Admin
        static bool ValidasiIDAdmin(string idAdmin, SqlConnection conn)
        {
            // Query SQL untuk memeriksa apakah ID Admin ada dalam database
            string query = "SELECT COUNT(*) FROM Data_Admin WHERE ID_Admin = @ID_Admin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID_Admin", idAdmin);

            // Eksekusi perintah dan periksa apakah ID Admin ditemukan
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }


        public void updateNamaAdmin(string idAdmin, string namaBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Admin SET Nama = @NamaBaru WHERE ID_Admin = @ID_Admin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NamaBaru", namaBaru);
            cmd.Parameters.AddWithValue("@ID_Admin", idAdmin);
            cmd.ExecuteNonQuery();
        }

        public void updateUsernameAdmin(string idAdmin, string usernameBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Admin SET Username = @UsernameBaru WHERE ID_Admin = @ID_Admin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UsernameBaru", usernameBaru);
            cmd.Parameters.AddWithValue("@ID_Admin", idAdmin);
            cmd.ExecuteNonQuery();
        }

        public void updatePasswordAdmin(string idAdmin, string passwordBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Admin SET Password = @PasswordBaru WHERE ID_Admin = @ID_Admin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PasswordBaru", passwordBaru);
            cmd.Parameters.AddWithValue("@ID_Admin", idAdmin);
            cmd.ExecuteNonQuery();
        }

        public void updateNoTeleponAdmin(string idAdmin, string noTeleponBaru, SqlConnection conn)
        {
            string query = "UPDATE Data_Admin SET No_Telepon = @NoTeleponBaru WHERE ID_Admin = @ID_Admin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NoTeleponBaru", noTeleponBaru);
            cmd.Parameters.AddWithValue("@ID_Admin", idAdmin);
            cmd.ExecuteNonQuery();
        }


        public void insertDokumen(string Jenis_Dokumen, string Tanggal_Terbit, string ID_admin, string ID_Penduduk, SqlConnection conn)
        {
            try
            {
                string query = "INSERT INTO Data_Dokumen ( Jenis_Dokumen, Tanggal_Terbit, ID_Admin, ID_Penduduk) VALUES (@Jenis_Dokumen, @Tanggal_Terbit, @ID_admin, @Id_Pdk)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Jenis_Dokumen", Jenis_Dokumen);
                    cmd.Parameters.AddWithValue("@Tanggal_Terbit", Tanggal_Terbit);
                    cmd.Parameters.AddWithValue("@ID_admin", ID_admin);
                    cmd.Parameters.AddWithValue("@Id_Pdk", ID_Penduduk);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\nData Dokumen berhasil ditambahkan.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("\nGagal menambahkan data dokumen: " + ex.Message);
            }
        }
        public void lihat(SqlConnection conn)
        {
            Console.WriteLine("+--------------------+---------------------------+------------------+----------------------+");
            Console.WriteLine("|   Jenis Dokumen    |      Tanggal Terbit       |     ID Admin     |      ID Penduduk     |");
            Console.WriteLine("+--------------------+---------------------------+------------------+----------------------+");

            SqlCommand cmd = new SqlCommand("SELECT Jenis_Dokumen, Tanggal_Terbit, ID_Admin, ID_Penduduk FROM Data_Dokumen", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Jenis_Dokumen = reader["Jenis_Dokumen"].ToString();
                    string Tanggal_Terbit = reader["Tanggal_Terbit"].ToString();
                    string ID_Admin = reader["ID_Admin"].ToString();
                    string ID_Penduduk = reader["ID_Penduduk"].ToString();

                    Console.WriteLine($"|   {Jenis_Dokumen,-14}   |   {Tanggal_Terbit,-14}   |    {ID_Admin,-10}    |   {ID_Penduduk,-10}   |");
                }
            }

            Console.WriteLine("+--------------------+---------------------------+------------------+-----------------------+");
        }

        private bool ValidasiUser(string username, string password)
        {
            // String koneksi untuk menghubungkan ke database
            string strKoneksi = $"Data source = LAPTOP-FJJPQU6D\\ANGGI_PUSPITA; initial catalog = PelayananMasyarakat; User ID = sa; password = anggi2022";

            try
            {
                // Membuka koneksi ke database
                using (SqlConnection conn = new SqlConnection(strKoneksi))
                {
                    conn.Open();
                    // Query SQL untuk memeriksa keberadaan pengguna dengan username dan password tertentu
                    string query = "SELECT COUNT(*) FROM Data_Admin WHERE Username = @Username AND Password = @Password";

                    // Membuat objek SqlCommand dengan query dan koneksi yang diberikan
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Menambahkan parameter-parameter ke query SQL
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = (int)cmd.ExecuteScalar(); // Mengambil jumlah baris yang cocok dengan username dan password
                        if (count > 0)
                        {
                            return true; // Mengembalikan true jika pengguna valid
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false; // Mengembalikan false jika pengguna tidak valid
        }
    }
}
