/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { UserInfoRm } from '../../models/user-info-rm';

export interface UsersUserInfoGet$Plain$Params {
}

export function usersUserInfoGet$Plain(http: HttpClient, rootUrl: string, params?: UsersUserInfoGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<UserInfoRm>> {
  const rb = new RequestBuilder(rootUrl, usersUserInfoGet$Plain.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<UserInfoRm>;
    })
  );
}

usersUserInfoGet$Plain.PATH = '/Users/User-info';
