🌱 ThePrototype - Farming System
Bu proje, Unity kullanılarak geliştirilen modüler bir tarım simülasyonu sistemidir. Oyuncular belirli alanlara ekin ekebilir, ekinlerin büyümesini takip edebilir ve olgunlaşan mahsulleri hasat edebilir.

Sistem event-driven (olay tabanlı) bir mimariye sahiptir ve modüler bir şekilde genişletilebilir.

📌 Özellikler
✅ Ekim Sistemi

Oyuncular belirli ekim alanlarına (grid tabanlı) mahsul ekebilir.
Kullanıcı dostu bir arayüz ile ekin seçimi yapılabilir.
✅ Büyüme Mekaniği

Her ekin belirli büyüme aşamalarından geçer.
Ekinlerin büyüme süresi, ScriptableObject (CropSO) ile özelleştirilebilir.
EventBus kullanılarak ekin ekildiğinde otomatik olarak büyüme süreci başlatılır.
✅ Hasat Sistemi

Büyüme tamamlandığında, ekinler hasat edilebilir hale gelir.
Hasat edilebilecek ekinler, özel bir UI ile kullanıcıya gösterilir.
Sürükleme (drag & drop) mekaniği ile hasat edilen ürünler toplanır.
✅ Dokunmatik & Mobil Uyumluluk

Mobil cihazlarda dokunmatik kontroller desteklenir.
Uzun basma (long press) mekanizması ile özel işlemler gerçekleştirilebilir.
✅ Event-Driven Mimari

EventBus yapısı kullanılarak oyun içi olaylar arasında bağlantı sağlanır.
Modüler ve genişletilebilir bir yapı sunar.
✅ Performans Optimizasyonu

Event tabanlı sistemler kullanılarak gereksiz hesaplamalar minimize edilmiştir.
Minimal UI update cycle kullanılarak UI performansı optimize edilmiştir.
⚙️ Kullanılan Teknolojiler & Sistem Mimarisi
🛠️ Teknolojiler
Unity Engine (Ana oyun motoru - Unity 2022.3+)
C# (Oyun mekaniği ve sistemler için ana dil)
Scriptable Objects (SO) (Veri yönetimi ve konfigürasyon için)
EventBus Yapısı (Oyun içi event yönetimi için)
Singleton Design Pattern (Global yöneticiler için Persistent Singleton kullanımı)
Physics Raycasting (UI ve dünya içi nesne etkileşimleri için)
🚀 Kurulum ve Kullanım
1️⃣ Projeyi Klonlayın

sh
Kopyala
Düzenle
git clone https://github.com/burakttorun/FarmSimulation.git
cd ThePrototype-FarmingSystem
2️⃣ Unity ile Açın
📌 Desteklenen Unity Versiyonu: Unity 2021.3+

3️⃣ Mobil İçin APK Build Alın

Unity’de File > Build Settings menüsüne gidin.
Platform olarak Android'i seçin ve Switch Platform yapın.
Build And Run ile APK dosyanızı oluşturun.
4️⃣ Oyunu Mobilde Test Edin

APK’yı cihazınıza yükleyerek dokunmatik kontrollerle test edebilirsiniz.
🎯 Temel Oyun Mekaniklerini Deneyin:
✅ Ekim – Bir ekin seçin ve haritaya yerleştirin.
✅ Büyüme – Büyüme aşamalarını takip edin.
✅ Hasat – Büyüyen mahsulleri toplayın.
🏗️ Kod Yapısı & Önemli Scriptler
📌 GrowthManager.cs

Ekinlerin büyüme sürecini yönetir.
Büyüme sürecinin tamamlanıp tamamlanmadığını kontrol eder.
📌 CropUIManager.cs

Kullanıcı arayüzü üzerinden ekin ekme ve hasat işlemlerini düzenler.
UI butonlarını oluşturur ve yönetir.
📌 HarvestEntityManager.cs

Hasat edilen ürünlerin sürükleme (drag & drop) mekanizmasını yönetir.
Ürünlerin toplanmasını ve envantere eklenmesini sağlar.
📌 EventBus.cs

Event-driven mimariyi desteklemek için kullanılır.
Sistemler arası iletişimi sağlar ve bileşenler arası bağımlılığı azaltır.
📌 CropSO.cs (Scriptable Object)

Ekinlerin verilerini saklar.
Büyüme süresi, görünüm ve hasat edilebilirlik durumu gibi bilgileri içerir.
📌 PlacementManager.cs

Ekinlerin nereye ekilebileceğini yönetir.
Raycast kullanarak uygun alanları tespit eder.

🎯 Sistem Akışı
1️⃣ Ekin Seçme → Kullanıcı, ekilecek mahsulü seçer.
2️⃣ Ekim → Seçilen mahsul belirlenen alana ekilir.
3️⃣ Büyüme Süreci → GrowthManager tarafından büyüme aşamaları tamamlanır.
4️⃣ Hasat Edilebilir Duruma Gelme → Son aşamaya gelen mahsul toplanabilir.
5️⃣ Hasat İşlemi → HarvestEntityManager sürükleyerek mahsulü toplama işlemini yönetir.






👨‍💻 Geliştirici: Burak Torun

🚀 İyi oyunlar! 🎮🌾