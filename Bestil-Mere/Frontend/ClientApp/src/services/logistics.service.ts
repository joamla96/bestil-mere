import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class LogisticsService {
  constructor(
    private http: HttpClient) { }

  public getAll() {
    return this.http.get(environment.api.logistics);
  }
}
