import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { StorageService } from './core/services/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'FreelanceNFControl';

  constructor(
    private primengConfig: PrimeNGConfig,
    public storageService: StorageService,
  ) {}

  ngOnInit() {
      this.primengConfig.ripple = true;
  }
}