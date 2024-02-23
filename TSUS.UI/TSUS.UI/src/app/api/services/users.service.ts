/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { UserInfoRm } from '../models/user-info-rm';
import { usersUserInfoGet$Json } from '../fn/users/users-user-info-get-json';
import { UsersUserInfoGet$Json$Params } from '../fn/users/users-user-info-get-json';
import { usersUserInfoGet$Plain } from '../fn/users/users-user-info-get-plain';
import { UsersUserInfoGet$Plain$Params } from '../fn/users/users-user-info-get-plain';

@Injectable({ providedIn: 'root' })
export class UsersService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `usersUserInfoGet()` */
  static readonly UsersUserInfoGetPath = '/Users/User-info';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `usersUserInfoGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  usersUserInfoGet$Plain$Response(params?: UsersUserInfoGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<UserInfoRm>> {
    return usersUserInfoGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `usersUserInfoGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  usersUserInfoGet$Plain(params?: UsersUserInfoGet$Plain$Params, context?: HttpContext): Observable<UserInfoRm> {
    return this.usersUserInfoGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<UserInfoRm>): UserInfoRm => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `usersUserInfoGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  usersUserInfoGet$Json$Response(params?: UsersUserInfoGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<UserInfoRm>> {
    return usersUserInfoGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `usersUserInfoGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  usersUserInfoGet$Json(params?: UsersUserInfoGet$Json$Params, context?: HttpContext): Observable<UserInfoRm> {
    return this.usersUserInfoGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<UserInfoRm>): UserInfoRm => r.body)
    );
  }

}
