package validation;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.HashMap;

/**
 * Unit test for simple App.
 */
class AppTest {

    // Validar se campos existem

    @Test
    void WhenTokensIsEmpty_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo msisdn não existe no registo.";

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenMsisdnFieldDoesNotExist_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo msisdn não existe no registo.";

        tokens.put("balance_before_topup", new Double(1000));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenBalanceBeforeTopupFieldDoesNotExist_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo balance_before_topup não existe no registo.";

        tokens.put("msisdn", "9123456");
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopupPropensity2FieldDoesNotExist_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_2 não existe no registo.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000));
        // tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopupPropensity3FieldDoesNotExist_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_3 não existe no registo.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000));
        tokens.put("topup_propensity_2", new Long(2));
        // tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopupPropensity4FieldDoesNotExist_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_4 não existe no registo.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        // tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    // Validar tipo de dados
    @Test
    void WhenBalanceBeforeTopupHasWrongTypeValue_AnExceptionShouldBeThrow() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_4 não existe no registo.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        // tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenBalanceBeforeTopup_DoesNotHave2DecimalPlaces_AnExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo balance_before_topup não é um double com duas casas decimais.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(5.555));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenBalanceBeforeTopup_DoesNotHave2DecimalPlaces_AnExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo balance_before_topup não é um double com duas casas decimais.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(-5.555));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    // Validar os limites dos valores dos campos
    @Test
    void WhenBalanceBeforeTopupValue_IsNotInRange_AnExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo balance_before_topup com o valor '-1000.01' não tem um valor superior ou igual a -1000 ou inferior ou igual a 1000.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(-1000.01));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenBalanceBeforeTopupValue_IsNotInRange_AnExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo balance_before_topup com o valor '1000.01' não tem um valor superior ou igual a -1000 ou inferior ou igual a 1000.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.01));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity2Value_IsNotInRange_AnExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_2 com o valor '101' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(101));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity2Value_IsNotInRange_AnExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_2 com o valor '-1' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(-1));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity3Value_IsNotInRange_AnExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_3 com o valor '101' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(101));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity3Value_IsNotInRange_AnExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_3 com o valor '-1' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(-1));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity4Value_IsNotInRange_AnExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_4 com o valor '101' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(101));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    @Test
    void WhenTopUpPropensity4Value_IsNotInRange_AnExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;
        String exceptionMessage = "O campo topup_propensity_4 com o valor '-1' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.";

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1000.00));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(-1));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(exception.getMessage(), exceptionMessage);
    }

    // Conjunto de testes que devem ter sucesso

    @Test
    void WhenFieldValues_AreValid_NoExceptionShouldBeThrow_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(1.15));
        tokens.put("topup_propensity_2", new Long(2));
        tokens.put("topup_propensity_3", new Long(3));
        tokens.put("topup_propensity_4", new Long(4));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(true, exception == null);
    }

    @Test
    void WhenFieldValues_AreValid_NoExceptionShouldBeThrow_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        HashMap<String, HashMap<String, HashMap<String, String>>> refdata = null;

        tokens.put("msisdn", "9123456");
        tokens.put("balance_before_topup", new Double(-1.15));
        tokens.put("topup_propensity_2", new Long(20));
        tokens.put("topup_propensity_3", new Long(30));
        tokens.put("topup_propensity_4", new Long(40));

        // Act
        Exception exception = null;
        try {
            App.validateFocusEvent(tokens, refdata);
        } catch (Exception ex) {
            exception = ex;
        }

        // Assert
        assertEquals(true, exception == null);
    }

    @Test
    void ForEventCondition_WhenBalance_IsInRange_ResultCondition_ShouldBeTrue() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("balance_before_topup", new Double(-1.15));

        // Act
        boolean result = App.eventBuilderEventConditionBalance(tokens);

        // Assert
        assertEquals(true, result);
    }

    @Test
    void ForEventCondition_WhenBalance_IsNotInRange_ResultCondition_ShouldBeFalse_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("balance_before_topup", new Double(-1000.01));

        // Act
        boolean result = App.eventBuilderEventConditionBalance(tokens);

        // Assert
        assertEquals(false, result);
    }

    @Test
    void ForEventCondition_WhenBalance_IsNotInRange_ResultCondition_ShouldBeFalse_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("balance_before_topup", new Double(1000.01));

        // Act
        boolean result = App.eventBuilderEventConditionBalance(tokens);

        // Assert
        assertEquals(false, result);
    }

    @Test
    void ForEventCondition_WhenPropensity2_IsInRange_ResultCondition_ShouldBeTrue() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("topup_propensity_2", new Long(20));

        // Act
        boolean result = App.eventBuilderEventConditionPropensity2(tokens);

        // Assert
        assertEquals(true, result);
    }

    @Test
    void ForEventCondition_WhenPropensity2_IsNotInRange_ResultCondition_ShouldBeFalse_1() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("topup_propensity_2", new Long(-1));

        // Act
        boolean result = App.eventBuilderEventConditionPropensity2(tokens);

        // Assert
        assertEquals(false, result);
    }

    @Test
    void ForEventCondition_WhenPropensity2_IsNotInRange_ResultCondition_ShouldBeFalse_2() {

        // Arrange
        HashMap<String, Object> tokens = new HashMap<>();
        tokens.put("topup_propensity_2", new Long(101));

        // Act
        boolean result = App.eventBuilderEventConditionPropensity2(tokens);

        // Assert
        assertEquals(false, result);
    }

}
