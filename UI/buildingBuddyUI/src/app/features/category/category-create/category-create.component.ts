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
  categoryToCreate: CategoryCreateRequest = {
    name: '',
    categoryFatherId: undefined
  }

  constructor(private categoryService: CategoryService, private router: Router) {}

  ngOnInit(): void {
    this.getAllCategories();
  }

  private getAllCategories() {
    this.categoryService.getAllCategories()
      .subscribe({
        next: (response) => {
          this.categories = response;
          console.log('Categories:', this.categories); 
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
          this.router.navigateByUrl('categories/list');
        }
      });
  }
}
