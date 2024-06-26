/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { BaseControlFlags } from '../../models/base-control-flags';
import { DepartmentRm } from '../../models/department-rm';

export interface DepartmentsGet$Plain$Params {
  controlFlags?: BaseControlFlags;
}

export function departmentsGet$Plain(http: HttpClient, rootUrl: string, params?: DepartmentsGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<DepartmentRm>>> {
  const rb = new RequestBuilder(rootUrl, departmentsGet$Plain.PATH, 'get');
  if (params) {
    rb.query('controlFlags', params.controlFlags, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<DepartmentRm>>;
    })
  );
}

departmentsGet$Plain.PATH = '/Departments';
