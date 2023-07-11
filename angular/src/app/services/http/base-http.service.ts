import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {lastValueFrom} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseHttpService {
  constructor(private http: HttpClient) {
  }

  sendRequest<T>(
    method: string,
    url: string,
    body?: any,
    headers?: HttpHeaders,
    params?: HttpParams
  ): Promise<T> {
    const options = {headers, params};
    const request = this.http.request<T>(method, url, {body, ...options});
    return lastValueFrom(request);
  }
}
