import { bootstrapApplication } from '@angular/platform-browser';
import { config } from './app/app.config.server';
import { LayoutComponent } from './app/components/layout-area/layout/layout.component';

const bootstrap = () => bootstrapApplication(LayoutComponent, config);

export default bootstrap;
