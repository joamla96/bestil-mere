import {Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {AuthService} from "./auth.service";
import {Observable} from "rxjs";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

	constructor(private authService: AuthService) {
	}

	addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
		return req.clone({setHeaders: {Authorization: `Bearer ${token}`}});
	}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (this.authService.isLoggedIn()) {
			return next.handle(this.addToken(req, this.authService.jwt()));
		}
	}
}
