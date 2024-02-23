/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { DepartmentRm } from '../models/department-rm';
import { departmentsAddPost } from '../fn/departments/departments-add-post';
import { DepartmentsAddPost$Params } from '../fn/departments/departments-add-post';
import { departmentsGet$Json } from '../fn/departments/departments-get-json';
import { DepartmentsGet$Json$Params } from '../fn/departments/departments-get-json';
import { departmentsGet$Plain } from '../fn/departments/departments-get-plain';
import { DepartmentsGet$Plain$Params } from '../fn/departments/departments-get-plain';

@Injectable({ providedIn: 'root' })
export class DepartmentsService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `departmentsGet()` */
  static readonly DepartmentsGetPath = '/Departments';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `departmentsGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  departmentsGet$Plain$Response(params?: DepartmentsGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<DepartmentRm>>> {
    return departmentsGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `departmentsGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  departmentsGet$Plain(params?: DepartmentsGet$Plain$Params, context?: HttpContext): Observable<Array<DepartmentRm>> {
    return this.departmentsGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<DepartmentRm>>): Array<DepartmentRm> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `departmentsGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  departmentsGet$Json$Response(params?: DepartmentsGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<DepartmentRm>>> {
    return departmentsGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `departmentsGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  departmentsGet$Json(params?: DepartmentsGet$Json$Params, context?: HttpContext): Observable<Array<DepartmentRm>> {
    return this.departmentsGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<DepartmentRm>>): Array<DepartmentRm> => r.body)
    );
  }

  /** Path part for operation `departmentsAddPost()` */
  static readonly DepartmentsAddPostPath = '/Departments/Add';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `departmentsAddPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  departmentsAddPost$Response(params?: DepartmentsAddPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return departmentsAddPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `departmentsAddPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  departmentsAddPost(params?: DepartmentsAddPost$Params, context?: HttpContext): Observable<void> {
    return this.departmentsAddPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

}
