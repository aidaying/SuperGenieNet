import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
export class ServerErrorComponent {
  error: any;

  constructor(private router: Router) { 
    const navidation = this.router.getCurrentNavigation();
    this.error = navidation?.extras?.state?.['error'];
  }
}
