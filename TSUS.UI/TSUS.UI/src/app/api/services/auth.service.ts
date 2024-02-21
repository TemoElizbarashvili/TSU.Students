/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { authChangePasswordPut } from '../fn/auth/auth-change-password-put';
import { AuthChangePasswordPut$Params } from '../fn/auth/auth-change-password-put';
import { authEmailPost } from '../fn/auth/auth-email-post';
import { AuthEmailPost$Params } from '../fn/auth/auth-email-post';
import { authLoginPost } from '../fn/auth/auth-login-post';
import { AuthLoginPost$Params } from '../fn/auth/auth-login-post';
import { authLoginPost$Plain } from '../fn/auth/auth-login-post-plain';
import { AuthLoginPost$Plain$Params } from '../fn/auth/auth-login-post-plain';
import { authRegistrationPost } from '../fn/auth/auth-registration-post';
import { AuthRegistrationPost$Params } from '../fn/auth/auth-registration-post';
import { authRequestPasswordResetGet } from '../fn/auth/auth-request-password-reset-get';
import { AuthRequestPasswordResetGet$Params } from '../fn/auth/auth-request-password-reset-get';
import { authRequestPasswordResetGet$Plain } from '../fn/auth/auth-request-password-reset-get-plain';
import { AuthRequestPasswordResetGet$Plain$Params } from '../fn/auth/auth-request-password-reset-get-plain';
import { authVerifyPut } from '../fn/auth/auth-verify-put';
import { AuthVerifyPut$Params } from '../fn/auth/auth-verify-put';

@Injectable({ providedIn: 'root' })
export class AuthService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `authRegistrationPost()` */
  static readonly AuthRegistrationPostPath = '/Auth/Registration';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authRegistrationPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authRegistrationPost$Response(params?: AuthRegistrationPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return authRegistrationPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authRegistrationPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authRegistrationPost(params?: AuthRegistrationPost$Params, context?: HttpContext): Observable<void> {
    return this.authRegistrationPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `authLoginPost()` */
  static readonly AuthLoginPostPath = '/Auth/Login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authLoginPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authLoginPost$Plain$Response(params?: AuthLoginPost$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<string>> {
    return authLoginPost$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authLoginPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authLoginPost$Plain(params?: AuthLoginPost$Plain$Params, context?: HttpContext): Observable<string> {
    return this.authLoginPost$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<string>): string => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authLoginPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authLoginPost$Response(params?: AuthLoginPost$Params, context?: HttpContext): Observable<StrictHttpResponse<string>> {
    return authLoginPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authLoginPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  authLoginPost(params?: AuthLoginPost$Params, context?: HttpContext): Observable<string> {
    return this.authLoginPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<string>): string => r.body)
    );
  }

  /** Path part for operation `authEmailPost()` */
  static readonly AuthEmailPostPath = '/Auth/Email';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authEmailPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  authEmailPost$Response(params?: AuthEmailPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return authEmailPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authEmailPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  authEmailPost(params?: AuthEmailPost$Params, context?: HttpContext): Observable<void> {
    return this.authEmailPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `authVerifyPut()` */
  static readonly AuthVerifyPutPath = '/Auth/Verify';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authVerifyPut()` instead.
   *
   * This method doesn't expect any request body.
   */
  authVerifyPut$Response(params?: AuthVerifyPut$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return authVerifyPut(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authVerifyPut$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  authVerifyPut(params?: AuthVerifyPut$Params, context?: HttpContext): Observable<void> {
    return this.authVerifyPut$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `authRequestPasswordResetGet()` */
  static readonly AuthRequestPasswordResetGetPath = '/Auth/RequestPasswordReset';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authRequestPasswordResetGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  authRequestPasswordResetGet$Plain$Response(params?: AuthRequestPasswordResetGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<number>> {
    return authRequestPasswordResetGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authRequestPasswordResetGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  authRequestPasswordResetGet$Plain(params?: AuthRequestPasswordResetGet$Plain$Params, context?: HttpContext): Observable<number> {
    return this.authRequestPasswordResetGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<number>): number => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authRequestPasswordResetGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  authRequestPasswordResetGet$Response(params?: AuthRequestPasswordResetGet$Params, context?: HttpContext): Observable<StrictHttpResponse<number>> {
    return authRequestPasswordResetGet(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authRequestPasswordResetGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  authRequestPasswordResetGet(params?: AuthRequestPasswordResetGet$Params, context?: HttpContext): Observable<number> {
    return this.authRequestPasswordResetGet$Response(params, context).pipe(
      map((r: StrictHttpResponse<number>): number => r.body)
    );
  }

  /** Path part for operation `authChangePasswordPut()` */
  static readonly AuthChangePasswordPutPath = '/Auth/ChangePassword';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authChangePasswordPut()` instead.
   *
   * This method doesn't expect any request body.
   */
  authChangePasswordPut$Response(params?: AuthChangePasswordPut$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return authChangePasswordPut(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authChangePasswordPut$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  authChangePasswordPut(params?: AuthChangePasswordPut$Params, context?: HttpContext): Observable<void> {
    return this.authChangePasswordPut$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

}
