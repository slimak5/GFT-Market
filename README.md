# GFT-Market

Proces uruchamiania: 

1. Klonujemy repozytorium https://github.com/slimak5/GFT-Market.git i w oknie Team Explorer robimy new branch: origin/master i zaznaczamy opcję Checkout.
2. 

PPM na solucję, Restore NuGet Packages

PPM -> Rebuild All

Jeśli baza danych nie zdeployowała się automatycznie (sprawdzamy w SQL Server Explorer, w (localdb)/ProjectsV13), robimy PPM na projekt bazy danch -> Publish

W folderze projektu jest dołączony plik GFT Market Database.mdf - zawiera dane o dostępnych produktach 
(albo szukamy w properties gdzie utworzyła się baza danych i podmieniamy plik, lub ręcznie dodajemy własne produkty ["BAK1","BAK2"] to dostępne instancje backendów, przypisujemy każdemy produktowi dowolną z nich)

Odpalamy przeglądarkę, klikamy "Start", czekamy aż wszystko się zbuduje i odpali na ExpressIIS i aplikacja jest gotowa
