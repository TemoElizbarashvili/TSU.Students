/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { facultiesAddPost } from '../fn/faculties/faculties-add-post';
import { FacultiesAddPost$Params } from '../fn/faculties/faculties-add-post';

@Injectable({ providedIn: 'root' })
export class FacultiesService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `facultiesAddPost()` */
  static readonly FacultiesAddPostPath = '/Faculties/Add';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `facultiesAddPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  facultiesAddPost$Response(params?: FacultiesAddPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return facultiesAddPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `facultiesAddPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  facultiesAddPost(params?: FacultiesAddPost$Params, context?: HttpContext): Observable<void> {
    return this.facultiesAddPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

}
