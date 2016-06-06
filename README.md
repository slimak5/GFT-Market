# GFT-Market

Proces uruchamiania: 
WAŻNE: Visual Studio musi być uruchomione z uprawnieniami administratora, w innym przypadku nie będzie wstanie udostępnić instancji serwisów backendowych. 

1. Klonujemy repozytorium https://github.com/slimak5/GFT-Market.git i w oknie Team Explorer robimy new branch: origin/master i zaznaczamy opcję Checkout.
2. Klikamy PPM na solucję i na Rebuild. (Jeśli pojawi się monit o "Restore NuGet Packages - Yes"
3. Jeśli nie będzie, klikamy PPM na solucję, "Manage Nuget Packages" i na "Restore NuGetPackages" jeśli wyskoczy powiadomienie na górze okna. Jeśli nie ma, oznacza że pakiety pobrały się z repozytorium i nie trzeba aktualizować. 
4. Klikamy PPM na projekt bazodanowy: folder GFT.Database -> projekt: GFT Market Database i na "Publish..." 
5. W okienku wybieramy "Load Profile" i ładujemy plik : GFT Market Database.publish.xml 
6. Wchodzimy w SQL Server Object Explorer (jeśli nie jest widoczny jest on w View -> Sql Server Object Explorer [CTRL+'\'+S]
7. Przechodzimy do lokalizacji: (localdb)\ProjectsV13\Databases\GFT Market Database\Tables\
8. Klikamy PPM na dbo.item i na "View Data"
9. W otwartym oknie edycji widzimy aktualnie dodane produkty i możemy dodawać nowe (baza prawdopodobnie będzie pusta)
10. Kolumna ItemId wypełnia się automatycznie, dodajemy nazwy oraz supportedServiceId które może być "BAK1" lub "BAK2" zależnie od instancji backendu jaki chcemy używać do obsługi
11. Uruchamiamy przeglądarkę która jest ustawiona jako domyslna.
12. Wracamy do Visual Studio i klikamy przycisk "Start" który zbuduje i uruchomi solucję. 
13. Visual powinien przechwycić proces przegądarki i odpalić w kartach UI, API i ExecutedTrades. Błedami 403.14 się nie przejmujemy, domyślnie odpalają sie główne strony serwera do których bezposrednio nie mamy dostępu. 
14. Po odczekaniu aż aplikacja się w pełni zbuduje przechodzimy na adres: http://localhost:49571/Views/main.html
15. jeżeli przedmioty się nie wyświetlają, czekamy chwilę i próbujemy odswieżyć stronę. 
16. aplikacja jest gotowa do działania, pobrała user id z serwera i jest podłączona do executed trades (możemy to sprawdzić w trybie debugowania przeglądarki)

Pierwsze pole od lewej jest to ilość produktu, drugie odpowiada za cenę. W odpowiedzi na transakcje t-id oznacza id transakcji a u-id id usera który sprzedał dane produkty.

