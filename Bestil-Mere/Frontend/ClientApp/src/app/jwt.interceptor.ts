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

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		// add authorization header with jwt token if available
		if (this.authService.isLoggedIn()) {
			request = request.clone({
				setHeaders: {
					Authorization: `Bearer ${this.authService.jwt}`
				}
			});
		}

		return next.handle(request);
	}
}
