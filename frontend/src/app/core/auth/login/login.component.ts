import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { StorageService } from '../../services/storage.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api';

@Component({
	templateUrl: './login.component.html'
})
export class LoginComponent {

	loginForm: FormGroup;
	submitted = false;
	forgotLinkHover = false;

	returnUrl: string;

	constructor(
		private router: Router,
		private authService: AuthService,
		private storageService: StorageService,
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private loaderService: NgxUiLoaderService,
		private messageService: MessageService,
	) {
		this.loginForm = this.formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', Validators.required]
		});

		this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
	}

	get email () { return this.loginForm.get('email')?.value }
  	get password () { return this.loginForm.get('password')?.value }

	login() {
		this.submitted = true;
		if (this.loginForm.valid) {
			this.loaderService.start();
			this.authService.login(this.email, this.password).subscribe({
				next: (response) => {
					console.log(response);
					this.storageService.saveUserData(response);
					this.router.navigate(['main']);
					this.loaderService.stop();
				},
				error: (fail) => {
					console.log(fail);
					this.loaderService.stop();
					this.showError(["Erro ao tentar autenticar o usuário"]);
				}
			})
		}
	}

	private showError(messages: string[]) {
		messages.forEach((message) => {
			this.messageService.add({ severity: 'error', summary: 'Erro!', detail: message, life: 5000 });
		})
	}

	handleRedirect() {
		this.returnUrl ?
			this.router.navigate([this.returnUrl]) :
			this.router.navigate(["/"]);
	}
}
