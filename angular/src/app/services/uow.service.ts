import { DevisArticleService } from './devisArticle.service';
import { FileUploadService } from './file.upload.service';
import { CommandeService } from './commande.service';
import { SousCategorieService } from 'src/app/services/sous.categorie.service';
import { CategorieService } from 'src/app/services/categorie.service';
import { FournisseurService } from 'src/app/services/fournisseur.service';
import { ArticleService } from './article.service';

import { Injectable } from '@angular/core';
import { DetailCmdService } from './detail-cmd.service';
import { FournitureService } from './fourniture.service';
import { UserService } from './user.service';
import { RoleService } from './role.service';
import { ClientService } from './client.service';
import { UniteService } from './unite.service';
import { AchatService } from './achat.service';
import { DevisService } from './devis.service';


@Injectable({
  providedIn: 'root'
})
export class UowService {
  achats = new AchatService();
  articles = new ArticleService();
  fournisseurs = new FournisseurService();
  categories = new CategorieService();
  sousCategories = new SousCategorieService();
  commandes = new CommandeService();
  fournitures = new FournitureService();
  detailCmds = new DetailCmdService();
  files = new FileUploadService();
  users = new UserService();
  clients = new ClientService();
  roles = new RoleService();
  unites2 = ['U', 'L', 'Kg'];
  unites = new UniteService();
  deviss = new DevisService();
  devisArticles = new DevisArticleService();
  typePayemet = ['éspece', 'Chèque', 'crédit'];
  profils = [
    { id: 1, name: 'Commercial', },
    { id: 2, name: 'Manager', },
    { id: 3, name: 'Administrateur' },
  ];
  years = [...Array(5).keys()].map(e => (new Date().getFullYear() - 3) + e + 1);
  months = [...Array(12).keys()].map(e => e + 1);
  monthsAlpha = [
    'Janvier',
    'Fevrier',
    'Mars',
    'Avril',
    'Mai',
    'Juin',
    'Juillet',
    'Août',
    'Septembre',
    'Octobre',
    'Novembre',
    'Décembre',
  ].map((e, i) => {
    return { id: i + 1, name: e };
  });
  monthsAlphaMin = [
    'Jan',
    'Fev',
    'Mars',
    'Avr',
    'Mai',
    'Juin',
    'Juil',
    'Août',
    'Sept',
    'Oct',
    'Nov',
    'Déc',
  ].map((e, i) => {
    return { id: i + 1, name: e };
  });

  i = 0;

  minutes = ['00', '30'];

  heurs = [...Array(21).keys()].map(e => {
    if (e % 2 === 0) {
      this.i++;
    }

    return `${(this.i + 8) < 10 ? '0' + (this.i + 8) : (this.i + 8)}`;
  });

  times = [...Array(21).keys()].map(e => `${this.heurs[e]}:${this.minutes[e % 2]}`);

  constructor() { }

  valideDate(date: Date): Date {
    if (date === null) {
      return null;
    }
    date = new Date(date);

    const hoursDiff = date.getHours() - date.getTimezoneOffset() / 60;
    const minutesDiff = (date.getHours() - date.getTimezoneOffset()) % 60;
    date.setHours(hoursDiff);
    date.setMinutes(minutesDiff);

    return date;
  }
}
