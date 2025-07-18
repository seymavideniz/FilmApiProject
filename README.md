FilmProject

Film verilerini TheMovieDB API'den alan, veritabanına kaydeden ve kullanıcılara film bilgileri sunan bir ASP.NET Core Web API projesidir.

 Özellikler

- Film, kategori, detay ve kullanıcı CRUD işlemleri

- TMDB API ile periyodik film güncelleme 

- Swagger UI ile test edilebilir API

- PostgreSQL veritabanı kullanımı

 Kurulum ve Çalıştırma
 
 1. Projeyi klonlayın:
 git clone https://github.com/seymavideniz/FilmProject.git
 cd FilmProject
 
 2. Bağımlılıkları yükleyin:
 dotnet restore
 
 3. appsettings.json ayarlarını yapın:
 "ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Port=5432;Database=FilmDb;Username=postgres;Password=1234"
 },
 "TMDB": {
   "ApiKey": "<api-key-buraya>",
   "BaseUrl": "https://api.themoviedb.org/3",
   "MaxFilms": 50
 }
 
 4. Veritabanı migrasyonları:
 dotnet ef database update
 
 5. Projeyi çalıştırın
 
 6. Swagger UI ile test edin
 
 
   - API Endpoints (Swagger) -
 
 Film

GET /api/films : Tüm filmleri getirir

GET /api/films/filtered : Filmleri filtrele

GET /api/films/details : Detaylı film bilgisi getirir

POST /api/films : Yeni film ekler

PUT /api/films/{id} : Film günceller

DELETE /api/films/{id} : Film siler

PUT /api/films/mark-watched/{filmId} : Filmi izlenmiş olarak işaretler

GET /api/films/watchlist : İzleme listesini getirir

📁 Kategori

GET /api/Category : Tüm kategorileri getirir

GET /api/Category/{id} : Belirli bir kategoriyi getirir

POST /api/Category : Yeni kategori ekler

DELETE /api/Category/{id} : Kategori siler

🎞️ Film Detayları

POST /api/FilmDetails/AddFilmDetails : Film detaylarını ekler

POST /api/FilmDetails/GetFilmDetails : Film detaylarını getirir

🔄 Güncelleme

POST /api/update : TMDB'den veri çekip veritabanına ekler

👤 Kullanıcı

POST /api/user/signup : Yeni kullanıcı oluşturur

POST /api/user/signin : Giriş yapar

PUT /api/user/{id} : Kullanıcı bilgilerini günceller


 
 - Kullanılan Teknolojiler -
 
 * ASP.NET Core Web API
 
 * Entity Framework Core
 
 * PostgreSQL
 
 * Swagger 
 
 
 
