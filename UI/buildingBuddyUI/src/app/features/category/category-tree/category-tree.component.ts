import { Component, Input } from '@angular/core';
import { Category } from '../interfaces/category';

@Component({
  selector: 'app-category-tree',
  template: `
    <ul class="category-list">
      <li *ngFor="let category of categories" class="category-item">
        <ng-container *ngIf="category.subCategories?.length; else individualCategory">
          <strong class="category-parent">Categoría Padre: {{ category.name }}</strong>
          <div>
            <em>Subcategorías de {{ category.name }}:</em>
            <app-category-tree [categories]="category.subCategories"></app-category-tree>
          </div>
        </ng-container>
        <ng-template #individualCategory>
          <strong class="category-individual">Categoría Individual: {{ category.name }}</strong>
        </ng-template>
      </li>
    </ul>
  `,
  styleUrls: ['./category-tree.component.css']
})
export class CategoryTreeComponent {
  private _categories: Category[] = [];

  @Input()
  set categories(value: Category[] | undefined) {
    this._categories = value || [];
  }

  get categories(): Category[] {
    return this._categories;
  }
}
