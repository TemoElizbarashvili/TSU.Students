/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';


export interface AuthChangePasswordPut$Params {
  userId?: number;
  password?: string;
  code?: number;
}

export function authChangePasswordPut(http: HttpClient, rootUrl: string, params?: AuthChangePasswordPut$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
  const rb = new RequestBuilder(rootUrl, authChangePasswordPut.PATH, 'put');
  if (params) {
    rb.query('userId', params.userId, {});
    rb.query('password', params.password, {});
    rb.query('code', params.code, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: '*/*', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
    })
  );
}

authChangePasswordPut.PATH = '/Auth/ChangePassword';
