import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { StorageService } from '../../services/storage.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { CUSTOM_ELEMENTS_SCHEMA, Directive, HostListener, Input } from '@angular/core';
import { of, throwError } from 'rxjs';

@Directive({
  selector: '[routerLink]',
})
class FakeRouterLink {
  @Input()
  routerLink = '';

  constructor(private router: Router) {}

  @HostListener('click')
  onClick() {
    this.router.navigate([this.routerLink]);
  }
}

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
  let storageServiceSpy = jasmine.createSpyObj('StorageService', ['saveUserData']);
  let loaderServiceSpy = jasmine.createSpyObj('NgxUiLoaderService', ['start', 'stop']);
  let messageServiceSpy = jasmine.createSpyObj('MessageService', ['add']);
  let routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl', 'navigate']);

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ButtonModule,
        InputTextModule,
        RippleModule,
        ReactiveFormsModule,
        PasswordModule,
        ToastModule
      ],
      declarations: [
        LoginComponent,
        FakeRouterLink
      ],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: StorageService, useValue: storageServiceSpy },
        { provide: NgxUiLoaderService, useValue: loaderServiceSpy },
        { provide: MessageService, useValue: messageServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();
  });

  beforeEach(async () => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('login() - when authService login with success, should do post success actions correctly', fakeAsync(() => {
    // Arrange
    component.loginForm = new FormGroup({
      email: new FormControl(['batman@email.com', Validators.required, Validators.email]),
      password: new FormControl('bat123*', Validators.required),
    });
    fixture.detectChanges();

    const response = {
      "userId": "a205e293-e692-4133-b09d-14b4a0924c7b",
      "accessToken": "jwt-token",
      "name": "Batman",
      "email": "batman@email.com"
    };
    authServiceSpy.login = jasmine.createSpy().and.returnValue(of(response));

    // Act
    component.login();
    tick();

    // Assert
    expect(storageServiceSpy.saveUserData).toHaveBeenCalledWith(response);
    expect(routerSpy.navigate.calls.first().args[0]).toEqual(["main"]);
    expect(loaderServiceSpy.stop).toHaveBeenCalled();
  }));

  it('login() - when authService login with fail, should do post fail actions correctly', fakeAsync(() => {
    // Arrange
    component.loginForm = new FormGroup({
      email: new FormControl(['batman@email.com', Validators.required, Validators.email]),
      password: new FormControl('bat123*', Validators.required),
    });
    fixture.detectChanges();

    authServiceSpy.login = jasmine.createSpy().and.returnValue(throwError(() => "Error"));

    // Act
    component.login();
    tick();

    // Assert
    expect(loaderServiceSpy.stop).toHaveBeenCalled();
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      { severity: 'error', summary: 'Erro!', detail: "Erro ao tentar autenticar o usuário", life: 5000 });
  }));
  
});
