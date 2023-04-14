import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.scss']
})
export class MenuBarComponent implements OnInit {

  items!: MenuItem[];

  activeItem!: MenuItem;

  constructor(
    private router: Router,
    public storageService: StorageService
  ) { }

  ngOnInit() {
    this.items = [
      {
        label: "Principal",
        icon: "pi pi-fw pi-home",
        command: () => this.router.navigate(['main'])
      },
      {
        label: 'Notas Fiscais',
        icon: 'pi pi-fw pi-calendar'
      },
      {
        label: 'Despesas',
        icon: 'pi pi-fw pi-pencil'
      },
      {
        label: 'Preferências',
        icon: 'pi pi-fw pi-cog'
      },
      {
        label: 'Sair',
        icon: 'pi pi-fw pi-sign-out',
        style: {'margin-left': 'auto'},
        command: () => {
          this.storageService.clean();
          this.router.navigate(['auth/login'])
        }
      }
  ];

    this.activeItem = this.items[0];
  }

}
