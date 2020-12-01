export class User {
  id = 0;
  nomComplete = 'user';
  email = 'user@angular.io';
  password = '123';
  tel = '00';
  idRole = 0;
  role = new Role();
}

export class Role {
  id = 0;
  nom = '';
  users: User[] = [];
}

export class Categorie {
  id = 0;
  libelle = '';
  image = '';
  sousCategories: SousCategorie[] = [];
}

export class SousCategorie {
  id = 0;
  libelle = '';
  idCategorie = 0;
  image = '';
  categorie = new Categorie();
  articles: Article[] = [];
}

export class Fournisseur {
  id = 0;
  nom = '';
  prenom = '';
  societe = '';
  telephone = '';
  fournitures: Fourniture[] = [];
  achats: Achat[] = [];
 }

export class Article {
  id = 0;
  code = '';
  titreFr = '';
  titreAr = '';
  emplacementMagasin = '';
  emplacementDepot = '';
  dateDernierAchat = new Date();
  prixUnitaire = 0;
  qteStock = 0;
  stockMin = 10;
  image = '';
  unite = 'U';
  bestSell = false;
  description = '';
  constructeur = '';
  idSousCategorie = 1;
  sousCategorie = new SousCategorie();
  fournitures: Fourniture[] = [];
  detailCmds: DetailCmd[] = [];
}

export class Achat {
  id = 0;
  idFournisseur = 0;
  montant = 0;
  modePayement = '';
  numCheque = '';
  credit = 0;
  avance = 0;
  date = new Date();
  fournisseur = new Fournisseur();
  fournitures: Fourniture[] = [];
}

export class Fourniture {
  id = 0;
  idArticle = 0;
  idFournisseur = 0;
  idAchat = 0;
  qte = 0;
  prixAchat = 0;
  typePayement = '';
  refTypePayement = 0;
  dateAchat = new Date();
  article = new Article();
  achat = new Achat();
  fournisseur = new Fournisseur();
}

export class Client {
  id = 0;
  nom = '';
  prenom = '';
  tel = '';
  email = '';
  adresse = '';
  ice = '';
  commandes: Commande[] = [];
}

export class Commande {
  id = 0;
  total = 0;
  idClient = 0;
  nomClient = 'Client comptoir';
  numCheque = '';
  credit = 0;
  avance = 0;
  modePayement = 'espéce';
  date = new Date();
  time = '00:00';
  client = new Client();
  // article = new Article();
  detailCmds: DetailCmd[] = [];
}

export class DetailCmd {
  idArticle = 0;
  idCommande = 0;
  prixVente = 0;
  qtePrise = 0;
  remise = 0;
  total = 0;
  article = new Article();
  commande = new Commande();
}

export class Devis {
  id = 0;
  client = '';
  date = new Date();
  montant = 0;

  devisActicles: DevisArticle[] = []
}

export class DevisArticle {
  id = 0;
  qte = 0;
  pu = 0;
  total = 0;
  remise = 0;
  idArticle = 0;
  idDevis = 0;
  article = new Article();
}
