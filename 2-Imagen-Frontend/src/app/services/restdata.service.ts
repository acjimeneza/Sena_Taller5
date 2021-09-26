import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {Time} from '../interfaces/time'

@Injectable({
  providedIn: 'root'
})
export class RestdataService {

  constructor(private httpClient: HttpClient) { }

  public getRandomTime(){
    return this.httpClient.get<Time>(`${environment.urlMicroservice}/api/v1/Time`);
  }

  public getEnvironmentVariable(environmentData: string){
    const options = { params: new HttpParams({fromString: `conf=${environmentData}` }) };
    return this.httpClient.get(`${environment.urlMicroservice}/api/v1/Environment`, { ...options, responseType: 'text' });
  }

  public getTime(){
    return this.httpClient.get<Time[]>(`${environment.urlMicroservice}/api/v1/Time/all`);
  }

  public setTime(body: Time){
    return this.httpClient.post<Time>(`${environment.urlMicroservice}/api/v1/Time`, body);
  }
}
