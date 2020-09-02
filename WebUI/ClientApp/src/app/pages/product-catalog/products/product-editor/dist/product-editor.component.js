"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.ProductEditorComponent = void 0;
var core_1 = require("@angular/core");
var product_model_1 = require("src/app/shared/models/product-catalog/product/product.model");
var unit_model_1 = require("src/app/shared/models/product-catalog/product/unit.model");
var config_1 = require("src/app/shared/confing/config");
var ProductEditorComponent = /** @class */ (function () {
    function ProductEditorComponent(productCatalogService, photoEditorService, core) {
        this.productCatalogService = productCatalogService;
        this.photoEditorService = photoEditorService;
        this.core = core;
        this.isEditing = false;
        this.product = new product_model_1.Product();
        this.brands = [];
        this.productCategories = [];
        this.BasePhotoUrl = config_1.Config.BasePhotoUrl;
    }
    ProductEditorComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.getBrands();
        this.getProductCategories();
        this.productCatalogService.productEditor.subscribe(function (res) {
            if (res.productRequestSuccess)
                return;
            if (res.product) {
                _this.getProductById(res.product.id);
            }
            else {
                _this.isEditing = false;
                _this.product = new product_model_1.Product();
            }
        });
    };
    ProductEditorComponent.prototype.getProductById = function (productId) {
        var _this = this;
        this.isEditing = true;
        this.productCatalogService.getProductById(productId).subscribe(function (res) {
            _this.product = res;
            _this.product.id = productId;
        });
    };
    ProductEditorComponent.prototype.getBrands = function () {
        var _this = this;
        this.productCatalogService.getAllBrands().subscribe(function (res) {
            var _a;
            (_a = _this.brands).push.apply(_a, res.data);
        });
    };
    ProductEditorComponent.prototype.getProductCategories = function () {
        var _this = this;
        this.productCatalogService.getAllProductCategories().subscribe(function (res) {
            var _a;
            (_a = _this.productCategories).push.apply(_a, res.data);
        });
    };
    ProductEditorComponent.prototype.openEditor = function () {
        this.product = new product_model_1.Product();
        this.productCatalogService.productEditor.next({ openEditor: true });
    };
    ProductEditorComponent.prototype.openClose = function () {
        this.productCatalogService.productEditor.next({ openEditor: false });
    };
    //#region Units
    ProductEditorComponent.prototype.addNewUnit = function () {
        this.product.units.push(new unit_model_1.Unit(true));
    };
    ProductEditorComponent.prototype.saveUnit = function (unit) {
        unit.price = +unit.price;
        unit.sellingPrice = +unit.sellingPrice;
        unit.weight = +unit.weight;
        unit.contentCount = +unit.contentCount;
        unit.count = +unit.count;
        if (unit.newAdded) {
            unit.productId = this.product.id;
            this.createUnit(unit);
        }
        else {
            this.updateUnit(unit);
        }
    };
    ProductEditorComponent.prototype.removeUnit = function (unit, unitIndex) {
        if (unit.newAdded)
            this.product.units.splice(unitIndex, 1);
        else
            this.deleteUnit(unit);
    };
    ProductEditorComponent.prototype.deleteUnit = function (unit) {
        var _this = this;
        this.productCatalogService.deleteUnit(unit, this.product.id).subscribe(function (res) {
            _this.core.showSuccessOperation();
            _this.product.units.splice(_this.product.units.indexOf(unit), 1);
        });
    };
    ProductEditorComponent.prototype.createUnit = function (unit) {
        var _this = this;
        this.productCatalogService.createUnit(unit, this.product.id).subscribe(function (res) {
            _this.getProductById(_this.product.id);
            _this.core.showSuccessOperation();
        });
    };
    ProductEditorComponent.prototype.updateUnit = function (unit) {
        var _this = this;
        this.productCatalogService.updateUnit(unit, this.product.id).subscribe(function (res) {
            _this.core.showSuccessOperation();
        });
    };
    //#endregion
    //#region Product
    ProductEditorComponent.prototype.createProduct = function () {
        var _this = this;
        this.productCatalogService.createProduct(this.product).subscribe(function (res) {
            _this.getProductById(res.result);
            _this.productCatalogService.productEditor.next({ productRequestSuccess: true, openEditor: true });
            _this.core.showSuccessOperation();
        });
    };
    ProductEditorComponent.prototype.updateProduct = function () {
        var _this = this;
        this.productCatalogService.updateProduct(this.product).subscribe(function (res) {
            _this.productCatalogService.productEditor.next({ productRequestSuccess: true, openEditor: true });
            _this.core.showSuccessOperation();
        });
    };
    ProductEditorComponent.prototype.saveData = function () {
        if (this.isEditing) {
            this.updateProduct();
        }
        else {
            this.createProduct();
        }
    };
    //#endregion
    ProductEditorComponent.prototype.changePhoto = function (photoUrl) {
        var _this = this;
        if (photoUrl === void 0) { photoUrl = null; }
        var dialogRef = this.photoEditorService.showPhotoEditor(photoUrl);
        dialogRef.afterClosed().subscribe(function (result) {
            if (!result)
                return;
            _this.product.photoUrl = result.imgUrl;
            if (_this.isEditing)
                _this.updateProduct();
        });
    };
    __decorate([
        core_1.Input('isEditorOpen')
    ], ProductEditorComponent.prototype, "isEditorOpen");
    ProductEditorComponent = __decorate([
        core_1.Component({
            selector: 'app-product-editor',
            templateUrl: './product-editor.component.html',
            styleUrls: ['./product-editor.component.scss']
        })
    ], ProductEditorComponent);
    return ProductEditorComponent;
}());
exports.ProductEditorComponent = ProductEditorComponent;
