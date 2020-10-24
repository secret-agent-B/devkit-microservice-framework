import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/services/auth.guard';

const routes: Routes = [
	{ path: '', redirectTo: 'home', pathMatch: 'full', canLoad: [AuthGuard] },
	{
		path: 'home',
		loadChildren: () =>
			import('./screens/home/home.module').then((m) => m.HomePageModule),
		canLoad: [AuthGuard],
	},
	{
		path: 'auth',
		loadChildren: () =>
			import('./screens/auth/auth.module').then((m) => m.AuthPageModule),
	},
	{
		path: 'signup',
		loadChildren: () =>
			import('./screens/signup/signup.module').then(
				(m) => m.SignupPageModule
			),
	},
	{
		path: 'welcome',
		loadChildren: () =>
			import('./screens/welcome/welcome.module').then(
				(m) => m.WelcomePageModule
			),
	},
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
	],
	exports: [RouterModule],
})
export class AppRoutingModule {}
