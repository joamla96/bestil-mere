import { Injectable } from '@angular/core';
import {Observable, of, throwError} from "rxjs";
import {catchError, first, switchMap} from "rxjs/operators";
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {JwtModel} from "./shared/JwtModel";
import {LoginModel} from "./shared/LoginModel";
import * as jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
	private baseUrl = environment.gateway;
	private JWT_MODEL: string = "jwt";

  constructor(private http: HttpClient) { }

	/**
	 * Post username and password to backend for getting a jwt token
	 * Returns observable of login result (true = logged in / false = could not log in)
	 * @param model
	 */
	login(model: LoginModel): Observable<boolean> {
		return this.http.post(this.baseUrl + 'auth/login', model)
			.pipe(first(), catchError(() => of(false)), switchMap((jwtModel: JwtModel) => {
				this.saveJwt(jwtModel); // Denne gemmer bare jwt i localstorage
				return of(!!jwtModel);
			}));
	}

	/**
	 * Saves the Jwt to localstorage if the jwtModel is valid
	 * @param jwtModel
	 */
	saveJwt(jwtModel: JwtModel): boolean {
		if (!!!jwtModel) { return; }
		// save to localstorage
		localStorage.setItem(this.JWT_MODEL, JSON.stringify(jwtModel));
		return;
	}

	isLoggedIn(): boolean {
		return !!(JSON.parse(localStorage.getItem(this.JWT_MODEL)));
	}

	get jwt(): string {
		const jwt = JSON.parse(localStorage.getItem(this.JWT_MODEL)) as JwtModel;
		return jwt.access_token;
	}

	get userEmail(): string {
		try{
      return jwt_decode(this.jwt).unique_name;
    }
    catch(Error){
        return null;
    }
	}

	get customerId(): string {
		try{
			return jwt_decode(this.jwt).id;
		}
		catch(Error){
			return null;
		}
	}
}
