import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './Interceptors/authInterceptor';
import { DepartmentsService } from './api/services';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [DepartmentsService,  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }]
})
export class AppComponent implements OnInit {
  title = 'TSUS.UI';

  constructor(private departmentService: DepartmentsService) { }
  ngOnInit(): void {
  }
}
