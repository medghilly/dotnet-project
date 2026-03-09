# Annuaire ISCAE Mauritanie

Application mobile développée avec **.NET MAUI** pour la gestion de l'annuaire de l'**Institut Supérieur de Comptabilité et d'Administration des Entreprises (ISCAE) - Mauritanie**.

## Aperçu

L'application permet à l'administration de gérer l'annuaire des enseignants et des étudiants de l'ISCAE Mauritanie, avec une interface moderne aux couleurs officielles de l'institut.

## Fonctionnalités

- **Authentification** : Accès sécurisé par login/mot de passe
- **Tableau de bord** : Statistiques globales (enseignants, étudiants, filières, départements)
- **Départements** : Visualisation des deux départements (MED et MQI) et leurs filières
- **Enseignants** : Liste, recherche, filtres par département et grade, ajout/modification/suppression
- **Étudiants** : Liste, recherche, filtres par département/niveau/statut, ajout/modification/suppression

## Structure du projet

```
IscaeAnnuaire/
├── Models/
│   ├── Departement.cs
│   ├── Filiere.cs
│   ├── Enseignant.cs
│   └── Etudiant.cs
├── Data/
│   └── DatabaseService.cs
├── Views/
│   ├── LoginPage.xaml
│   ├── AccueilPage.xaml
│   ├── DepartementsPage.xaml
│   ├── EnseignantsPage.xaml
│   ├── EtudiantsPage.xaml
│   ├── DetailEnseignantPage.xaml
│   ├── DetailEtudiantPage.xaml
│   ├── FormulaireEnseignantPage.xaml
│   └── FormulaireEtudiantPage.xaml
├── App.xaml.cs
├── MauiProgram.cs
└── AppShell.xaml.cs
```

## Technologies utilisées

| Technologie | Version |
|-------------|---------|
| .NET MAUI | .NET 9 |
| SQLite | sqlite-net-pcl |
| C# | 12 |
| Visual Studio | 2022 |

## Base de données SQLite

### Tables

| Table | Description |
|-------|-------------|
| `Departements` | MED et MQI |
| `Filieres` | 10 filières (8 Licences + 2 Masters) |
| `Enseignants` | Enseignants avec grade et département |
| `Etudiants` | Étudiants avec filière, niveau et statut |

### Filières disponibles

**Département MED** (Management, Economie et Droit)
- Banques & Assurances
- Finance & Comptabilité
- Gestion des Ressources Humaines
- Techniques Commerciales et Marketing
- Master Pro. Finance et Comptabilité

**Département MQI** (Méthodes Quantitatives et Informatiques)
- Développement Informatique
- Informatique de Gestion
- Réseaux Informatiques et Télécommunications
- Statistique Appliquée à l'Economie
- Master Pro. Informatique Appliquée à la Gestion

## Identifiants par défaut

| Champ | Valeur |
|-------|--------|
| Nom d'utilisateur | `admin` |
| Mot de passe | `iscae2025` |

## Installation

1. Cloner le dépôt :
```bash
git clone https://github.com/votre-username/IscaeAnnuaire.git
```

2. Ouvrir le projet dans **Visual Studio 2022**

3. Installer les packages NuGet :
```powershell
Install-Package sqlite-net-pcl
Install-Package SQLitePCLRaw.bundle_green
```

4. Connecter un appareil Android ou lancer un émulateur

5. Lancer l'application avec **F5**

## Couleurs officielles

| Couleur | Code HEX |
|---------|----------|
| Bleu ISCAE | `#1B2A6B` |
| Vert ISCAE | `#2ECC71` |
| Fond | `#F5F5F5` |

## Contact ISCAE

- **Email** : contact@iscae.mr
- **Téléphone** : +222 45 24 19 66
- **Site web** : [www.iscae.mr](https://www.iscae.mr)

---

Développé avec ❤️ pour l'ISCAE Mauritanie
