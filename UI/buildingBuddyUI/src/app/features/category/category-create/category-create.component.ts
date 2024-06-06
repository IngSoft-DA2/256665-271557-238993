import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Router } from '@angular/router';
import { CategoryCreateRequest } from '../interfaces/category-create-request';
import { Category } from '../interfaces/category';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css']
})
export class CategoryCreateComponent implements OnInit {
  categories: Category[] = [];
  allCategories: Category[] = []; // Array to store all categories and subcategories

  categoryToCreate: CategoryCreateRequest = {
    name: '',
    categoryFatherId: undefined
  };

  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories()
      .subscribe({
        next: (response) => {
          this.categories = response;
          this.addSubCategories(response);
          console.log('Categories:', this.categories);
          console.log('All Categories:', this.allCategories);
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
        }
      });
  }

  createCategory(): void {
    this.categoryService.createCategory(this.categoryToCreate)
      .subscribe({
        next: () => {
          alert("Category Created With Success");
          this.loadCategories();
          this.resetCategoryForm();
        }
      });
  }


  private addSubCategories(categories: Category[]): void
  {
    this.addSubCategoriesRec(categories);
  }

  private addSubCategoriesRec(categoryList: Category[]) {
    this.allCategories = []; // Reset the array for the next iteration
    categoryList.forEach(category => {
      this.allCategories.push(category);
      if (category.subCategories && category.subCategories.length > 0) {
        this.addSubCategoriesRec(category.subCategories);
      }
    });
  };

  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target.value;
    this.categoryToCreate.categoryFatherId = value === 'null' ? undefined : value;
  }

  private resetCategoryForm(): void {
    this.categoryToCreate = {
      name: '',
      categoryFatherId: undefined
    };
  }
}
