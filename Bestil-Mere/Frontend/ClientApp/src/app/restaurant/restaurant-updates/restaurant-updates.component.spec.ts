import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantUpdatesComponent } from './restaurant-updates.component';

describe('RestaurantUpdatesComponent', () => {
  let component: RestaurantUpdatesComponent;
  let fixture: ComponentFixture<RestaurantUpdatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RestaurantUpdatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RestaurantUpdatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
