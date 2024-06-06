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
  allCategories: Category[] = [];
  mainCategories: Category[] = []; // Array para almacenar categorías principales

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
          this.allCategories = response;
          this.mainCategories = this.allCategories.filter(category => !category.categoryFatherId); // Filtrar categorías principales
          this.addSubCategories(this.allCategories);
          
          console.log('All Categories:', this.allCategories);
          console.log('Categories sin duplicar:', this.mainCategories);
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
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
        }
      });
  }

  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.categoryToCreate.categoryFatherId = value === '' ? undefined : value;
  }

  private resetCategoryForm(): void {
    this.categoryToCreate = {
      name: '',
      categoryFatherId: undefined
    };
  }


  private addSubCategories(categories: Category[]): void {
    this.allCategories = []; // Reiniciar el array para la próxima iteración
    this.addSubCategoriesRec(categories);
  }
  
  private addSubCategoriesRec(categoryList: Category[]): void {
    categoryList.forEach(category => {
      this.allCategories.push(category);
      if (category.subCategories && category.subCategories.length > 0) {
        this.addSubCategoriesRec(category.subCategories);
      }
    });
  }



}
