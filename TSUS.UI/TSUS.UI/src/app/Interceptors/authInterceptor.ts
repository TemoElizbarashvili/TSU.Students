import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       const token = localStorage.getItem('token');

       if(token){
        req = req.clone({
            setHeaders: {Authorization: `bearer ${token}`},
        });
       }
      return next.handle(req);
    }
}