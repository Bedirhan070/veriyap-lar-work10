using System;

class HashTable
{
    const int TABLE_SIZE = 100;  // Hash tablosunun boyutu
    int[] keys = new int[TABLE_SIZE];  // Hash tablosunu temsil eden dizi
    Random random = new Random();  // Rastgele anahtar değerleri oluşturmak için Random sınıfı

    public HashTable()
    {
        // Tablodaki tüm değerleri başlangıçta -1 yaparak boş olarak işaretliyoruz
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            keys[i] = -1;
        }
    }

    // Hash fonksiyonu (division method kullanılıyor)
    int HashFunction(int key)
    {
        // Anahtarı tablo boyutuna bölerek index hesaplanıyor
        return key % TABLE_SIZE;
    }

    // Linear Probing ile anahtar yerleştirme
    public bool InsertLinearProbing(int key)
    {
        int index = HashFunction(key);  // İlk hash indexi hesaplanıyor
        int initialIndex = index;  // İlk index kaydediliyor
        int step = 0;  // Adım sayacı

        // Çakışma varsa boş yer bulunana kadar lineer olarak ilerliyoruz
        while (keys[index] != -1)
        {
            step++;  // Adım sayısını artır
            index = (initialIndex + step) % TABLE_SIZE;  // Yeni index hesapla
            if (step >= TABLE_SIZE)
            {
                // Tabloda boş yer kalmadıysa hata mesajı ver ve false döndür
                Console.WriteLine("Tablo dolu, anahtar yerleştirilemedi: " + key);
                return false;
            }
        }
        keys[index] = key;  // Boş yer bulundu, anahtarı yerleştir
        return true;
    }

    // Quadratic Probing ile anahtar yerleştirme
    public bool InsertQuadraticProbing(int key)
    {
        int index = HashFunction(key);  // İlk hash indexi hesaplanıyor
        int initialIndex = index;  // İlk index kaydediliyor
        int step = 0;  // Adım sayacı

        // Çakışma varsa boş yer bulunana kadar karesel olarak ilerliyoruz
        while (keys[index] != -1)
        {
            step++;  // Adım sayısını artır
            index = (initialIndex + step * step) % TABLE_SIZE;  // Yeni index hesapla
            if (step >= TABLE_SIZE)
            {
                // Tabloda boş yer kalmadıysa hata mesajı ver ve false döndür
                Console.WriteLine("Tablo dolu, anahtar yerleştirilemedi: " + key);
                return false;
            }
        }
        keys[index] = key;  // Boş yer bulundu, anahtarı yerleştir
        return true;
    }

    // Hash tablosunu yazdıran fonksiyon
    public void PrintTable()
    {
        Console.WriteLine("Hash Tablosu:");
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            // Her bir index ve değerini yazdır
            if (keys[i] != -1)
                Console.WriteLine("Index " + i + ": " + keys[i]);
            else
                Console.WriteLine("Index " + i + ": boş");
        }
    }

    // Rastgele anahtar değerleri üret ve hash tablosuna ekle
    public void PopulateWithRandomKeys(bool useLinearProbing)
    {
        for (int i = 0; i < 100; i++)
        {
            int randomKey = random.Next(1, 1000);  // 1 ile 1000 arasında rastgele anahtar üret
            if (useLinearProbing)
            {
                InsertLinearProbing(randomKey);  // Linear probing ile yerleştir
            }
            else
            {
                InsertQuadraticProbing(randomKey);  // Quadratic probing ile yerleştir
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Linear Probing kullanarak hash tablosunu doldur
        HashTable hashTableLinear = new HashTable();
        hashTableLinear.PopulateWithRandomKeys(true);
        Console.WriteLine("Linear Probing ile oluşturulan Hash Tablosu:");
        hashTableLinear.PrintTable();

        // Quadratic Probing kullanarak hash tablosunu doldur
        HashTable hashTableQuadratic = new HashTable();
        hashTableQuadratic.PopulateWithRandomKeys(false);
        Console.WriteLine("\nQuadratic Probing ile oluşturulan Hash Tablosu:");
        hashTableQuadratic.PrintTable();
    }
}