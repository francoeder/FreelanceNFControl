import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './core/guards/authentication.guard';

const routes: Routes = [
  { path: 'auth', loadChildren: () => import('./core/auth/auth.module').then(m => m.AuthModule) },
  { path: 'main', canActivate: [AuthenticationGuard], loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule) },
  { path: 'invoices', canActivate: [AuthenticationGuard], loadChildren: () => import('./modules/invoices/invoices.module').then(m => m.InvoicesModule) },
  { path: 'expenses', canActivate: [AuthenticationGuard], loadChildren: () => import('./modules/expenses/expenses.module').then(m => m.ExpensesModule) },
  { path: 'preferences', canActivate: [AuthenticationGuard], loadChildren: () => import('./modules/preferences/preferences.module').then(m => m.PreferencesModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
