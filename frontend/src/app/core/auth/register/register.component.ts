import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { StorageService } from '../../services/storage.service';
import { AuthService } from '../../services/auth.service';
import { MessageService } from 'primeng/api';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
	templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {

	registerForm: FormGroup;
	submitted = false;

	constructor(
		private router: Router,
		private formBuilder: FormBuilder,
		private storageService: StorageService,
		private authService: AuthService,
		private messageService: MessageService,
		private loaderService: NgxUiLoaderService,
	) {
		this.registerForm = this.formBuilder.group({
			name: ['', Validators.required],
			lastName: ['', Validators.required],
			email: ['', [Validators.required, Validators.email]],
			password: ['', Validators.required],
			confirmPassword: ['', Validators.required],
		},
		{
			validator: ConfirmPasswordValidator("password", "confirmPassword")
		});
	}

	get name() { return this.registerForm.get('name')?.value }
	get lastName() { return this.registerForm.get('lastName')?.value }
	get email () { return this.registerForm.get('email')?.value }
  	get password () { return this.registerForm.get('password')?.value }
  	get confirmPassword () { return this.registerForm.get('confirmPassword')?.value }

	ngOnInit(): void {}

	register() {
		this.submitted = true;

		if (this.registerForm.valid) {
			this.loaderService.start();

			this.authService.register(this.name, this.lastName, this.email, this.password, this.confirmPassword).subscribe({
					next: (response) => {
						this.showSuccess("Cadastro efetuado com sucesso.");
						this.router.navigate(['/auth/login']);
						this.loaderService.stop();
					},
					error: (fail) => {
						console.log(fail);
						this.loaderService.stop();
						this.showError(["Erro ao tentar cadastrar o usuário"]);
					}
				});
		}
	}

	private showError(messages: string[]) {
		messages.forEach((message) => {
			this.messageService.add({ severity: 'error', summary: 'Erro!', detail: message, life: 5000 });
		})
	}

	private showSuccess(message: string) {
		this.messageService.add({severity: 'success', summary: 'Sucesso!', detail: message, life: 5000});
	}
}
