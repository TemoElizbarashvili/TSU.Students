/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { UserInfoRm } from '../../models/user-info-rm';

export interface UsersUserInfoGet$Json$Params {
}

export function usersUserInfoGet$Json(http: HttpClient, rootUrl: string, params?: UsersUserInfoGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<UserInfoRm>> {
  const rb = new RequestBuilder(rootUrl, usersUserInfoGet$Json.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<UserInfoRm>;
    })
  );
}

usersUserInfoGet$Json.PATH = '/Users/User-info';
