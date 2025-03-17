import { HttpInterceptorFn } from '@angular/common/http';


// Interceptor to add the Authorization header with the token to each HTTP request
export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem("token");
    console.log(token);
    const clonedRequest = req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
    });

    return next(clonedRequest);
};
