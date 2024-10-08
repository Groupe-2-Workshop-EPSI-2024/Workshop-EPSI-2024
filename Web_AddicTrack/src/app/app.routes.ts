import { Routes } from '@angular/router';
import { ProfilComponent } from './profil/profil.component';
import { HomeComponent } from './home/home.component';
import { ConnectionComponent } from './connection/connection.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'connection', component: ConnectionComponent },
    { path: 'profil', component: ProfilComponent },
    { path: '**', redirectTo: 'home' },
];